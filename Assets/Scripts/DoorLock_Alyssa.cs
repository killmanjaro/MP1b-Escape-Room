using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorLock_Alyssa : MonoBehaviour
{
    public Animator doorAnimator;
    private bool isUnlocked = false;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.lKey.wasPressedThisFrame)
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

        // The scoreboard decides when to change scenes (only after all 3 locks).
        ProgressScoreboard.Instance.RegisterLockSolved(this);
    }
}