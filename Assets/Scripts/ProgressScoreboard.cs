using UnityEngine;
using TMPro;

public class ProgressScoreboard : MonoBehaviour
{
    public static ProgressScoreboard Instance;

    public TextMeshProUGUI scoreboardText;
    public int totalLocks = 3;
    private int locksSolved = 0;

    public event System.Action<int, int> ProgressChanged;

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
        ProgressChanged?.Invoke(locksSolved, totalLocks);  
        UpdateDisplay();
        Debug.Log("Lock registered. Total solved: " + locksSolved);

        if (locksSolved >= totalLocks)
        {
            Debug.Log("All locks solved — win condition ready to fire!");
        }
    }

    void UpdateDisplay()
    {
        if (scoreboardText != null)   
            scoreboardText.text = $"Locks: {locksSolved} / {totalLocks}";
    }
}