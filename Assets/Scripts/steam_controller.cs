using System.Collections;
using UnityEngine;

public class SteamWall : MonoBehaviour
{
    [SerializeField] ParticleSystem steam;
    [SerializeField] Renderer[] veils;
    [SerializeField] Collider barrier;
    [SerializeField] float fadeDuration = 2.5f;

    Material[] mats;
    float[] startAlphas;

    void Awake()
    {
        mats = new Material[veils.Length];
        startAlphas = new float[veils.Length];

        for (int i = 0; i < veils.Length; i++)
        {
            mats[i] = veils[i].material;   // .material gives each wall its own instance
            startAlphas[i] = mats[i].GetColor("_BaseColor").a;
        }
    }

    public void Dissolve()
    {
        if (barrier != null) barrier.enabled = false;
        if (steam != null) steam.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        StopAllCoroutines();
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float k = 1f - Mathf.SmoothStep(0f, 1f, t / fadeDuration);

            for (int i = 0; i < mats.Length; i++)
            {
                Color c = mats[i].GetColor("_BaseColor");
                c.a = startAlphas[i] * k;
                mats[i].SetColor("_BaseColor", c);
            }
            yield return null;
        }

        gameObject.SetActive(false);
    }
}