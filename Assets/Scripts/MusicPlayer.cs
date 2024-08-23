using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] musicTracks;
    private AudioSource musicSource;

    void Awake()
    {
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        musicSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayRandomTrack();
    }

    private void Update()
    {
        if (!musicSource.isPlaying)
        {
            PlayRandomTrack();
        }
    }

    private void PlayTrack(int i)
    {
        musicSource.clip = musicTracks[i];
        musicSource.Play();
    }

    private void PlayRandomTrack()
    {
        int i = Random.Range(0, musicTracks.Length);
        PlayTrack(i);
    }
}
