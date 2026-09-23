using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressScoreboard : MonoBehaviour
{
    public static ProgressScoreboard Instance;

    public TextMeshProUGUI scoreboardText;
    public int totalLocks = 3;
    public int totalKeys = 3;          // watch, claim tag, painting key

    public string nextSceneName = "Scene_Brian";

    private int locksSolved = 0;
    private int keysFound = 0;

    // Each lock script can only be counted once, no matter how many
    // times it reports (prevents phantom or duplicate unlocks).
    private readonly HashSet<Object> solvedLocks = new HashSet<Object>();

    // Other scripts (like DoorLights) can listen for lock progress.
    // Sends (locksSolved, totalLocks).
    public event System.Action<int, int> ProgressChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateDisplay();
    }

    // Called by KeyPickup (on each key PREFAB) the first time it is grabbed
    public void RegisterKeyFound()
    {
        if (keysFound >= totalKeys) return;
        keysFound++;
        UpdateDisplay();
        Debug.Log("Key found. Total found: " + keysFound);
    }

    // Each lock script calls: ProgressScoreboard.Instance.RegisterLockSolved(this);
    public void RegisterLockSolved(Object source)
    {
        if (LossTimer.IsOver) return;

        if (!solvedLocks.Add(source))
        {
            Debug.Log("Ignored duplicate lock report from " + source.name);
            return;
        }

        locksSolved = solvedLocks.Count;
        ProgressChanged?.Invoke(locksSolved, totalLocks);
        UpdateDisplay();
        Debug.Log("Lock registered by " + source.name + ". Total solved: " + locksSolved);

        if (locksSolved >= totalLocks)
        {
            Debug.Log("All locks solved — loading " + nextSceneName);
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        }
    }

    void UpdateDisplay()
    {
        if (scoreboardText != null)
            scoreboardText.text =
                $"Keys found: {keysFound} / {totalKeys}\nLocks solved: {locksSolved} / {totalLocks}";
    }
}