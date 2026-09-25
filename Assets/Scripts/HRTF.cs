using UnityEngine;
using SteamAudio;

public class SetHRTF : MonoBehaviour
{
    [SerializeField] int hrtfIndex = 1;

    void Start()
    {
        if (SteamAudioManager.Singleton != null)
            SteamAudioManager.Singleton.currentHRTF = hrtfIndex;
    }
}