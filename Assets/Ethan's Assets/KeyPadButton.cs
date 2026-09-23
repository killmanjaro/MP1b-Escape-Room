using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeypadButton : MonoBehaviour
{
    [SerializeField] private int buttonNumber;
    [SerializeField] private KeypadPuzzle keypadManager;

    // Call this via XR Interactable Event (Select Entered or Hover Entered)
    public void OnPress()
    {
        if (keypadManager != null)
        {
            keypadManager.PressButton(buttonNumber);
        }
    }
}