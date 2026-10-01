using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRSimpleInteractable))]
public class DetentWheel : MonoBehaviour
{
    [SerializeField] Transform wheel;
    [SerializeField] Transform housing;
    [SerializeField] int detents = 4;
    [SerializeField] int correctIndex = 0;
    [SerializeField] float snapDuration = 0.25f;
    [SerializeField] UnityEvent onSettled;

    XRSimpleInteractable interactable;
    UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor grabber;
    float grabStartHandAngle;
    Quaternion grabStartRotation;
    Coroutine snapping;

    public int CurrentIndex { get; private set; }
    public bool IsCorrect => CurrentIndex == correctIndex;

    void Awake()
    {
        if (wheel == null) wheel = transform;
        if (housing == null) housing = wheel.parent;
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnGrab);
        interactable.selectExited.AddListener(OnRelease);
    }

    void OnDestroy()
    {
        if (interactable == null) return;
        interactable.selectEntered.RemoveListener(OnGrab);
        interactable.selectExited.RemoveListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if (snapping != null)
        {
            StopCoroutine(snapping);
            snapping = null;
        }
        grabber = args.interactorObject;
        grabStartHandAngle = HandAngle();
        grabStartRotation = wheel.localRotation;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        grabber = null;
        float step = 360f / detents;
        int index = Mathf.RoundToInt(wheel.localEulerAngles.z / step);
        CurrentIndex = ((index % detents) + detents) % detents;
        Debug.Log($"{name} settled at index {CurrentIndex}");
        snapping = StartCoroutine(SnapTo(CurrentIndex * step));
    }

    void Update()
    {
        if (grabber == null) return;
        float delta = Mathf.DeltaAngle(grabStartHandAngle, HandAngle());
        wheel.localRotation = grabStartRotation * Quaternion.AngleAxis(delta, Vector3.forward);
    }

    float HandAngle()
    {
        Vector3 local = housing.InverseTransformPoint(grabber.transform.position);
        return Mathf.Atan2(local.y, local.x) * Mathf.Rad2Deg;
    }

    IEnumerator SnapTo(float targetZ)
    {
        Quaternion from = wheel.localRotation;
        Quaternion to = Quaternion.Euler(0f, 0f, targetZ);
        float t = 0f;
        while (t < snapDuration)
        {
            t += Time.deltaTime;
            wheel.localRotation = Quaternion.Slerp(from, to, Mathf.SmoothStep(0f, 1f, t / snapDuration));
            yield return null;
        }
        wheel.localRotation = to;
        snapping = null;
        onSettled.Invoke();
    }
}