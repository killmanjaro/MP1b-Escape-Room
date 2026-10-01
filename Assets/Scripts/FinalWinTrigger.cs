using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class FinalWinTrigger : MonoBehaviour
{
    [SerializeField] private string winSceneName = "Scene_Win";
    [SerializeField] private float delayBeforeTransition = 1.5f;
    private bool hasTriggered = false;

    public void TriggerWin()
    {
        if (hasTriggered) return;
        hasTriggered = true;

        Invoke(nameof(LoadWinScene), delayBeforeTransition);
    }

    public void OnPlaceholderInteracted(SelectEnterEventArgs args)
    {
        TriggerWin();
    }

    private void LoadWinScene()
    {
        if (!string.IsNullOrEmpty(winSceneName))
        {
            SceneManager.LoadScene(winSceneName);
        }
    }
}