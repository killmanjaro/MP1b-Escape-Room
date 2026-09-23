using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CompartmentLock : MonoBehaviour
{
    public Animator compartmentAnimator;
    public GameObject claimTagPrefab;
    public Transform claimTagSpawnPoint;
    private bool isUnlocked = false;

    void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            Debug.Log("P WAS PRESSED");
            OnWatchInserted(default);
        }
    }

    public void OnWatchInserted(SelectEnterEventArgs args)
    {
        Debug.Log("OnWatchInserted called. isUnlocked = " + isUnlocked);

        if (isUnlocked) return;
        isUnlocked = true;

        if (compartmentAnimator != null)
        {
            compartmentAnimator.SetTrigger("Open");
            Debug.Log("Animator trigger fired");
        }
        else
        {
            Debug.Log("No animator assigned - skipping animation");
        }

        if (claimTagPrefab != null && claimTagSpawnPoint != null)
        {
            Instantiate(claimTagPrefab, claimTagSpawnPoint.position, claimTagSpawnPoint.rotation);
            Debug.Log("Claim tag spawned");
        }
        else
        {
            Debug.Log("No claim tag prefab/spawn point assigned — skipping");
        }

        ProgressScoreboard.Instance.RegisterLockSolved(this);
    }
}