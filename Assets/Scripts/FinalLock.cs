using UnityEngine;
using UnityEngine.Events;

public class FinalLock : MonoBehaviour
{
    [SerializeField] DetentWheel symbolWheel;
    [SerializeField] DetentWheel colorWheel;
    [SerializeField] UnityEvent onUnlocked;
    bool done;

    public void Check()
    {
        if (done) return;
        if (symbolWheel == null || colorWheel == null) return;
        if (!symbolWheel.IsCorrect || !colorWheel.IsCorrect) return;
        done = true;
        onUnlocked.Invoke();
    }
}