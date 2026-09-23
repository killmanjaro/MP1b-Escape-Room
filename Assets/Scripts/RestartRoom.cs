using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartRoom : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}