using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class KeyPickup : MonoBehaviour
{
    XRGrabInteractable grab;
    bool counted;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrabbed);
    }

    void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrabbed);
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        if (counted) return;      // only count the first grab of this key
        counted = true;

        if (ProgressScoreboard.Instance != null)
            ProgressScoreboard.Instance.RegisterKeyFound();
    }
}