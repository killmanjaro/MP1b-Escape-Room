using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class GrabGlow : MonoBehaviour
{
    [Header("Look (keep the same values on every grabbable!)")]
    public Color glowColor = new Color(1f, 0.85f, 0.2f); // warm yellow
    public float idleIntensity = 0.6f;
    public float pulseAmount = 0.3f;     // how much the idle glow breathes
    public float pulseSpeed = 2f;
    public float hoverIntensity = 2.5f;
    public float fadeSpeed = 8f;         // eases the brightness changes

    XRGrabInteractable grab;
    Renderer[] rends;
    bool hovered, held;
    float current;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        rends = GetComponentsInChildren<Renderer>();
        foreach (var r in rends)
            foreach (var m in r.materials)      // instances, so objects don't share glow
                m.EnableKeyword("_EMISSION");
    }

    void OnEnable()
    {
        grab.hoverEntered.AddListener(_ => hovered = true);
        grab.hoverExited.AddListener(_ => hovered = false);
        grab.selectEntered.AddListener(_ => held = true);
        grab.selectExited.AddListener(_ => held = false);
    }

    void OnDisable()
    {
        grab.hoverEntered.RemoveAllListeners();
        grab.hoverExited.RemoveAllListeners();
        grab.selectEntered.RemoveAllListeners();
        grab.selectExited.RemoveAllListeners();
    }

    void Update()
    {
        float target = held ? 0f
            : hovered ? hoverIntensity
            : idleIntensity + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;

        current = Mathf.Lerp(current, target, Time.deltaTime * fadeSpeed);

        Color c = glowColor * Mathf.Max(0f, current);
        foreach (var r in rends)
            foreach (var m in r.materials)
                m.SetColor("_EmissionColor", c);
    }
}