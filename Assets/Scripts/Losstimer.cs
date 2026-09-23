using UnityEngine;
using TMPro;


public class LossTimer : MonoBehaviour
{
    // Other scripts can check this (static, so it is reset in Awake)
    public static bool IsOver { get; private set; }

    public float startSeconds = 600f;
    public TextMeshProUGUI timerText;
    public Color normalColor = Color.white;
    public Color warningColor = Color.red;
    public float warningBelowSeconds = 30f;

    public GameObject lossMessage;
    public float messageDistance = 1.5f;

    float remaining;

    void Awake()
    {
        IsOver = false;
        remaining = startSeconds;
        if (lossMessage != null) lossMessage.SetActive(false);
    }

    void Update()
    {
        if (IsOver) return;

        remaining -= Time.deltaTime;
        if (remaining <= 0f)
        {
            remaining = 0f;
            Lose();
        }
        UpdateText();
    }

    void UpdateText()
    {
        if (timerText == null) return;
        int minutes = (int)(remaining / 60f);
        int seconds = (int)(remaining % 60f);
        timerText.text = $"{minutes}:{seconds:00}";
        timerText.color = remaining <= warningBelowSeconds ? warningColor : normalColor;
    }

    void Lose()
    {
        IsOver = true;
        Debug.Log("Time's up: player lost");

        if (lossMessage == null) return;
        Camera cam = Camera.main;
        if (cam != null)
        {
            Vector3 flatForward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;
            Vector3 pos = cam.transform.position + flatForward * messageDistance;
            lossMessage.transform.position = pos;
            lossMessage.transform.rotation = Quaternion.LookRotation(pos - cam.transform.position);
        }
        lossMessage.SetActive(true);
    }
}