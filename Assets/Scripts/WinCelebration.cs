using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class WinCelebration : MonoBehaviour
{
    public static WinCelebration Instance;

    public bool triggerWhenAllLocksSolved = true;

    [Header("Change 1: lights recolor")]
    public Light[] lights;
    public Color winLightColor = new Color(1f, 0.85f, 0.3f);

    public Transform door;
    public Vector3 doorOpenOffset = new Vector3(0f, 0f, 1.5f); // local units

    public ParticleSystem confetti;
    public UnityEvent onWin;

    [Header("You Win message (world-space canvas, start inactive)")]
    public GameObject winMessage;
    public float messageDistance = 2f;

    [Header("Timing")]
    public float duration = 1.5f;

    bool won;

    void Awake()
    {
        Instance = this;
        if (winMessage != null) winMessage.SetActive(false);
    }

    void Start()
    {
        // Start (not Awake) so ProgressScoreboard.Instance already exists.
        if (triggerWhenAllLocksSolved && ProgressScoreboard.Instance != null)
            ProgressScoreboard.Instance.ProgressChanged += OnProgress;
    }

    void OnDestroy()
    {
        if (ProgressScoreboard.Instance != null)
            ProgressScoreboard.Instance.ProgressChanged -= OnProgress;
    }

    void OnProgress(int solved, int total)
    {
        if (solved >= total) Celebrate();
    }

    public void Celebrate()
    {
        if (won) return;
        won = true;
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        PlaceMessage();
        if (confetti != null) confetti.Play();
        onWin.Invoke();

        // Remember starting values
        Color[] startColors = new Color[lights.Length];
        for (int i = 0; i < lights.Length; i++) startColors[i] = lights[i].color;
        Vector3 doorStart = door != null ? door.localPosition : Vector3.zero;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float k = Mathf.SmoothStep(0f, 1f, t / duration); // eased in and out

            for (int i = 0; i < lights.Length; i++)
                lights[i].color = Color.Lerp(startColors[i], winLightColor, k);
            if (door != null)
                door.localPosition = doorStart + doorOpenOffset * k;

            yield return null;
        }
    }

    void PlaceMessage()
    {
        if (winMessage == null) return;
        Camera cam = Camera.main;
        if (cam != null)
        {
            Vector3 flatForward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;
            Vector3 pos = cam.transform.position + flatForward * messageDistance;
            winMessage.transform.position = pos;
            winMessage.transform.rotation = Quaternion.LookRotation(pos - cam.transform.position);
        }
        winMessage.SetActive(true);
    }
}