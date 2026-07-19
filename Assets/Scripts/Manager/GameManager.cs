#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}