using UnityEngine;

public class BlacklightReveal : MonoBehaviour
{
    [SerializeField] Light[] lights;
    [SerializeField] Color targetLightColor = new Color(0.45f, 0.15f, 0.95f);
    [SerializeField] float intensityMultiplier = 0.35f;
    [SerializeField] Renderer[] messageRenderers;
    [SerializeField] Color messageEmission = new Color(0.65f, 0.3f, 1f);
    [SerializeField] float emissionBoost = 4f;
    [SerializeField] AudioSource revealAudio;
    [SerializeField] GameObject[] enableOnReveal;

    Material[] messageMats;

    void Awake()
    {
        messageMats = new Material[messageRenderers.Length];
        for (int i = 0; i < messageRenderers.Length; i++)
        {
            messageMats[i] = messageRenderers[i].material;
            messageMats[i].EnableKeyword("_EMISSION");
            messageMats[i].globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
            messageRenderers[i].enabled = false;
        }
    }

    public void Reveal()
    {
        if (revealAudio != null) revealAudio.Play();

        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].color = targetLightColor;
            lights[i].intensity *= intensityMultiplier;
        }

        Color target = messageEmission * emissionBoost;
        for (int i = 0; i < messageMats.Length; i++)
        {
            messageMats[i].SetColor("_EmissionColor", target);
            messageRenderers[i].enabled = true;
        }
	for (int i = 0; i < enableOnReveal.Length; i++)
	{
            enableOnReveal[i].SetActive(true);
	}
    }
}