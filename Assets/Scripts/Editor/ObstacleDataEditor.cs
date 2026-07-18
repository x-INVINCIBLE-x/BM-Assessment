#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleData))]
public class ObstacleDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ObstacleData obstacleData = target as ObstacleData;

        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();

        if (obstacleData.GridSettings == null)
        {
            EditorGUILayout.HelpBox("Assign Grid Settings", MessageType.Warning);
            return;
        }

        obstacleData.EnsureArraySize();
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Obstacle Matrix");

        Vector2Int size = obstacleData.GridSize;
        for (int z = size.y - 1; z >= 0; z--)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < size.x; x++)
            {
                GUI.backgroundColor =
                    obstacleData.IsBlocked(x, z) ? Color.red : Color.white;

                if (GUILayout.Button($"{x},{z}"))
                {
                    Undo.RecordObject(obstacleData, "Toggle Obstacle Tile");
                    obstacleData.ToggleBlocked(x, z);
                    EditorUtility.SetDirty(obstacleData);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        GUI.backgroundColor = Color.white;

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Clear All"))
        {
            obstacleData.ClearAll();
            EditorUtility.SetDirty(obstacleData);
        }

        EditorGUILayout.Space(10);

        EditorGUILayout.HelpBox("Red = Obstacle\nWhite = Walkable", MessageType.Info);
        return;

    }
}
#endif