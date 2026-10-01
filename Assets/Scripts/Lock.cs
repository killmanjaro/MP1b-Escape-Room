using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRSocketInteractor))]
public class Lock : MonoBehaviour
{
    [SerializeField] XRGrabInteractable correctKey;
    [SerializeField] Vector3 keyPositionOffset = Vector3.zero;
    [SerializeField] Vector3 keyRotationOffset = Vector3.zero;
    [SerializeField] Vector3 rotationAxis = Vector3.forward;
    [SerializeField] float rotationAngle = 360f;
    [SerializeField] float rotationDuration = 2f;
    [SerializeField] SteamSequence steam;

    XRSocketInteractor socket;
    bool solved;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnInserted);
    }

    void OnDestroy()
    {
        if (socket != null)
            socket.selectEntered.RemoveListener(OnInserted);
    }

    void OnInserted(SelectEnterEventArgs args)
    {
        if (solved) return;
        if (args.interactableObject as XRGrabInteractable != correctKey) return;

        solved = true;
        StartCoroutine(TurnAndOpen());
    }

    IEnumerator TurnAndOpen()
    {
        correctKey.enabled = false;
        socket.socketActive = false;

        yield return null;

        Transform pivot = socket.attachTransform;
        Transform key = correctKey.transform;

        Rigidbody rb = correctKey.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }

        Vector3 worldScale = key.lossyScale;

        key.SetParent(pivot, false);
        key.localPosition = keyPositionOffset;
        key.localRotation = Quaternion.Euler(keyRotationOffset);

        Vector3 parentScale = pivot.lossyScale;
        key.localScale = new Vector3(
            parentScale.x != 0f ? worldScale.x / parentScale.x : worldScale.x,
            parentScale.y != 0f ? worldScale.y / parentScale.y : worldScale.y,
            parentScale.z != 0f ? worldScale.z / parentScale.z : worldScale.z);

        Quaternion start = pivot.localRotation;
        Vector3 axis = rotationAxis.sqrMagnitude > 0f ? rotationAxis.normalized : Vector3.forward;

        float t = 0f;
        while (t < rotationDuration)
        {
            t += Time.deltaTime;
            float angle = Mathf.SmoothStep(0f, rotationAngle, t / rotationDuration);
            pivot.localRotation = start * Quaternion.AngleAxis(angle, axis);
            yield return null;
        }
        pivot.localRotation = start * Quaternion.AngleAxis(rotationAngle, axis);

        if (steam != null) steam.Advance();
    }
}