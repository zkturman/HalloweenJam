using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    private static MusicHandler musicHandlerSingleton;
    [SerializeField]
    private AudioClip[] musicTracks;
    private int currentTrack = -1; //prevents setting current track to zero index
    private AudioSource musicSource;
    [SerializeField]
    private float fadeTimeInSeconds = 0.5f;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);    
        if (musicHandlerSingleton == null)
        {
            musicHandlerSingleton = this;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void SetAudioSource(AudioSource musicSource)
    {
        this.musicSource = musicSource;
    }

    public void StartTrackWithoutRestart(int trackNumber)
    {
        if(currentTrack != trackNumber)
        {
            StartCoroutine(playTrack(trackNumber));
        }
    }   

    public void StartTrackWithRestart(int trackNumber)
    {
        StartCoroutine(playTrack(trackNumber));
    }

    private IEnumerator playTrack(int trackNumber)
    {
        float targetVolume = musicSource.volume;
        if (musicSource.isPlaying)
        {
            yield return StartCoroutine(fadeOutTrack());
        }
        musicSource!.clip = musicTracks[trackNumber];
        musicSource!.Play();
        StartCoroutine(fadeInTrack(targetVolume));
        currentTrack = trackNumber;
    }

    private IEnumerator fadeOutTrack()
    {
        float startingVolume = musicSource.volume;
        float fadeRate = 1f / fadeTimeInSeconds;
        for (float x = 0f; x <= 1f; x += Time.deltaTime * fadeRate)
        {
            musicSource.volume = Mathf.Lerp(startingVolume, 0f, x);
            yield return null;
        }
        musicSource.volume = 0f;
    }

    private IEnumerator fadeInTrack(float targetVolume)
    {
        musicSource.volume = 0f;
        float fadeRate = 1f / fadeTimeInSeconds;
        for (float x = 0f; x <= 1f; x += Time.deltaTime * fadeRate)
        {
            musicSource.volume = Mathf.Lerp(0f, targetVolume, x);
            yield return null;
        }
        musicSource.volume = targetVolume;
    }
}
