using System.Collections;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    [SerializeField] private Transform doorHinge;
    [SerializeField] private string keyTag = "Key";
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 2f;

    private bool isUnlocked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isUnlocked || !other.CompareTag(keyTag)) return;

        isUnlocked = true;
        Destroy(other.gameObject); // Destroy the key
        StartCoroutine(RotateDoorOpen());
    }

    private IEnumerator RotateDoorOpen()
    {
        Quaternion startRotation = doorHinge.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, openAngle, 0);
        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime * openSpeed;
            doorHinge.rotation = Quaternion.Slerp(startRotation, targetRotation, progress);
            yield return null;
        }
    }
}