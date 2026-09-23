using System.Collections;
using UnityEngine;

/// <summary>
/// Escape signifier: indicator lights above the exit door.
/// All start red. Each solved lock eases the next light to green
/// (count-based, so it never reveals WHICH lock was solved).
/// </summary>
public class DoorLights : MonoBehaviour
{
    [Tooltip("Assign the light renderers, left to right. Should match totalLocks.")]
    public Renderer[] lights;

    public Color offColor = new Color(0.8f, 0.05f, 0.05f);   // red = unsolved
    public Color onColor = new Color(0.1f, 0.9f, 0.2f);      // green = solved
    public float emissionIntensity = 1.0f;
    public float fadeDuration = 0.6f;

    int lightsOn = 0;

    void Start()
    {
        // Start (not Awake/OnEnable) so ProgressScoreboard.Instance already exists.
        foreach (var l in lights) SetColor(l, offColor);

        if (ProgressScoreboard.Instance != null)
            ProgressScoreboard.Instance.ProgressChanged += OnProgressChanged;
    }

    void OnDestroy()
    {
        if (ProgressScoreboard.Instance != null)
            ProgressScoreboard.Instance.ProgressChanged -= OnProgressChanged;
    }

    void OnProgressChanged(int solved, int total)
    {
        while (lightsOn < solved && lightsOn < lights.Length)
        {
            StartCoroutine(FadeLight(lights[lightsOn], offColor, onColor));
            lightsOn++;
        }
    }

    IEnumerator FadeLight(Renderer r, Color from, Color to)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            // SmoothStep = eased in AND out (not linear)
            float k = Mathf.SmoothStep(0f, 1f, t / fadeDuration);
            SetColor(r, Color.Lerp(from, to, k));
            yield return null;
        }
        SetColor(r, to);
    }

    void SetColor(Renderer r, Color c)
    {
        var m = r.material;               // material instance per light
        m.EnableKeyword("_EMISSION");
        m.SetColor("_BaseColor", c);      // URP Lit
        m.SetColor("_EmissionColor", c * emissionIntensity);
    }
}