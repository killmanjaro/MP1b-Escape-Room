using UnityEngine;
using UnityEngine.Events;

public class SteamSequence : MonoBehaviour
{
    [SerializeField] SteamWall[] walls;
    [SerializeField] UnityEvent onAllCleared;

    int index = 0;

    [ContextMenu("Advance")]
    public void Advance()
    {
        if (index >= walls.Length) return;

        if (index + 1 < walls.Length)
            walls[index + 1].gameObject.SetActive(true);

        walls[index].Dissolve();
        index++;

        if (index >= walls.Length)
            onAllCleared.Invoke();
    }
}