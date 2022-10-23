using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    private MusicHandler musicHandler;
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private int defaultSceneTrack;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startMusicAfterFirstFrame());
    }

    private IEnumerator startMusicAfterFirstFrame()
    {
        yield return new WaitForEndOfFrame();
        musicHandler = FindObjectOfType<MusicHandler>();
        if (musicSource == null)
        {
            musicSource = musicHandler.gameObject.GetComponent<AudioSource>();
        }
        musicHandler.SetAudioSource(musicSource);
        musicHandler.StartTrackWithoutRestart(defaultSceneTrack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
