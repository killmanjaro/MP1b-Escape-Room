using UnityEngine;
using TMPro;

public class ProgressScoreboard : MonoBehaviour
{
    public static ProgressScoreboard Instance;

    public TextMeshProUGUI scoreboardText;
    public int totalLocks = 3;
    private int locksSolved = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateDisplay();
    }

    public void RegisterLockSolved()
    {
        locksSolved++;
        UpdateDisplay();
        Debug.Log("Lock registered. Total solved: " + locksSolved);

        if (locksSolved >= totalLocks)
        {
            Debug.Log("All locks solved — win condition ready to fire!");
        }
    }

    void UpdateDisplay()
    {
        scoreboardText.text = $"Locks: {locksSolved} / {totalLocks}";
    }
}