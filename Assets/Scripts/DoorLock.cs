using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class DoorLock : MonoBehaviour
{
    public Animator doorAnimator;
    public string nextSceneName;
    public float delayBeforeTransition = 1.5f;
    private bool isUnlocked = false;

    void Update()
    {
        // TEMP: debug trigger for testing without VR
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            Debug.Log("L WAS PRESSED");
            OnKeyInserted(default);
        }
    }

    public void OnKeyInserted(SelectEnterEventArgs args)
    {
        Debug.Log("OnKeyInserted called. isUnlocked = " + isUnlocked);

        if (isUnlocked) return;
        isUnlocked = true;

        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
            Debug.Log("Door animator trigger fired");
        }
        else
        {
            Debug.Log("No animator assigned — skipping animation");
        }

        ProgressScoreboard.Instance.RegisterLockSolved();

        Invoke(nameof(LoadNextScene), delayBeforeTransition);
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Debug.Log("Loading scene: " + nextSceneName);
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("DoorLock: No next scene name assigned!");
        }
    }
}