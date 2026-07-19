#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

/// <summary>
/// Handles application-level actions such as exiting the game.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Exits Play Mode in the Unity Editor or quits the built application.
    /// </summary>
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}