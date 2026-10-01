using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSequence : MonoBehaviour
{
    [SerializeField] AudioSource knockSource;
    [SerializeField] AudioSource breakSource;
    [SerializeField] Renderer fadeQuad;
    [SerializeField] GameObject winMessage;

    [SerializeField] float knockLength = 4f;
    [SerializeField] float gapBetweenKnocks = 1f;
    [SerializeField] float firstVolume = 0.5f;
    [SerializeField] float secondVolume = 1f;
    [SerializeField] float pauseBeforeBlack = 2f;
    [SerializeField] float fadeDuration = 0.4f;
    [SerializeField] float pauseBeforeBreak = 2f;
    [SerializeField] float pauseBeforeMessage = 2f;

    [SerializeField] string nextSceneName = "Scene_Win";

    Material fadeMat;
    bool started;

    void Awake()
    {
        fadeMat = fadeQuad.material;
        SetFade(0f);
        fadeQuad.gameObject.SetActive(false);
        if (winMessage != null) winMessage.SetActive(false);
    }

    public void Begin()
    {
        if (started) return;
        started = true;
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        yield return Knock(firstVolume);
        yield return new WaitForSeconds(gapBetweenKnocks);
        yield return Knock(secondVolume);
        yield return new WaitForSeconds(pauseBeforeBlack);

        fadeQuad.gameObject.SetActive(true);

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetFade(Mathf.SmoothStep(0f, 1f, t / fadeDuration));
            yield return null;
        }
        SetFade(1f);

        yield return new WaitForSeconds(pauseBeforeBreak);

        if (breakSource != null) breakSource.Play();

        yield return new WaitForSeconds(pauseBeforeMessage);

        if (winMessage != null) winMessage.SetActive(true);

        if (!string.IsNullOrEmpty(nextSceneName))
            SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator Knock(float volume)
    {
        knockSource.volume = volume;
        knockSource.time = 0f;
        knockSource.Play();
        yield return new WaitForSeconds(knockLength);
        knockSource.Stop();
    }

    void SetFade(float a)
    {
        Color c = fadeMat.GetColor("_BaseColor");
        c.a = a;
        fadeMat.SetColor("_BaseColor", c);
    }
}