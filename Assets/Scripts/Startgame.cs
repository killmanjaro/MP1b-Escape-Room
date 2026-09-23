using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [Tooltip("Exact name of the escape room scene (must be in Build Settings)")]
    public string roomSceneName = "Scene_Ethan";

    public void StartGame_OnClick()
    {
        if (string.IsNullOrEmpty(roomSceneName))
        {
            Debug.LogWarning("StartGame: no room scene name assigned!");
            return;
        }
        Debug.Log("Starting game: loading " + roomSceneName);
        SceneManager.LoadScene(roomSceneName);
    }
}