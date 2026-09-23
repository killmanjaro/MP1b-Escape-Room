using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class BriefcaseLock : MonoBehaviour
{
    public Animator briefcaseAnimator;
    public GameObject journalPrefab;
    public Transform journalSpawnPoint;
    public GameObject binocularsPrefab;
    public Transform binocularsSpawnPoint;
    private bool isUnlocked = false;

    void Update()
    {
        // TEMP: debug trigger for testing without VR
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            Debug.Log("O WAS PRESSED");
            OnClaimTagInserted(default);
        }
    }

    public void OnClaimTagInserted(SelectEnterEventArgs args)
    {
        Debug.Log("OnClaimTagInserted called. isUnlocked = " + isUnlocked);

        if (isUnlocked) return;
        isUnlocked = true;

        if (briefcaseAnimator != null)
        {
            briefcaseAnimator.SetTrigger("Open");
            Debug.Log("Briefcase animator trigger fired");
        }
        else
        {
            Debug.Log("No animator assigned — skipping animation");
        }

        if (journalPrefab != null && journalSpawnPoint != null)
        {
            Instantiate(journalPrefab, journalSpawnPoint.position, journalSpawnPoint.rotation);
            Debug.Log("Journal spawned");
        }
        else
        {
            Debug.Log("No journal prefab/spawn point assigned — skipping");
        }

        if (binocularsPrefab != null && binocularsSpawnPoint != null)
        {
            Instantiate(binocularsPrefab, binocularsSpawnPoint.position, binocularsSpawnPoint.rotation);
            Debug.Log("Binoculars spawned");
        }
        else
        {
            Debug.Log("No binoculars prefab/spawn point assigned — skipping");
        }

        ProgressScoreboard.Instance.RegisterLockSolved(this);
    }
}