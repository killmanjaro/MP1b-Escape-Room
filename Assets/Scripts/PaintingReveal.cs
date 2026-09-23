using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PaintingReveal : MonoBehaviour
{
    public Animator paintingAnimator;
    public GameObject finalKeyPrefab;
    public Transform finalKeySpawnPoint;
    private bool isRevealed = false;

    public void OnPaintingSelected(SelectEnterEventArgs args)
    {
        if (isRevealed) return;
        isRevealed = true;

        if (paintingAnimator != null)
        {
            paintingAnimator.SetTrigger("Open");
            Debug.Log("Painting swing animation triggered");
        }

        if (finalKeyPrefab != null && finalKeySpawnPoint != null)
        {
            Instantiate(finalKeyPrefab, finalKeySpawnPoint.position, finalKeySpawnPoint.rotation);
            Debug.Log("Key spawned behind painting");
        }
    }
}