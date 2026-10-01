using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEvent : MonoBehaviour
{
    [SerializeField] float delay = 10f;
    [SerializeField] UnityEvent onElapsed;

    public void Begin()
    {
        StopAllCoroutines();
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        yield return new WaitForSeconds(delay);
        onElapsed.Invoke();
    }
}