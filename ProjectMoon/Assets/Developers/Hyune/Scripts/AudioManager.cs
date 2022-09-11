using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public float fadeDuration;

    public AudioClip currentMusic;

    AudioSource musicChannel;
    AudioSource sfxChannel1;
    AudioSource sfxChannel2;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            musicChannel = GetComponents<AudioSource>()[0];
            sfxChannel1 = GetComponents<AudioSource>()[1];
            sfxChannel2 = GetComponents<AudioSource>()[2];

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {

    }

    void ChangeMusicVol(float vol)
    {
        musicChannel.volume = vol;
    }

    void ChangeSFXVol(float vol)
    {
        sfxChannel1.volume = vol;
        sfxChannel2.volume = vol;
    }

    void ChangeMusic(AudioClip clip)
    {
        currentMusic = clip;
        musicChannel.Stop();
        musicChannel.clip = clip;
        musicChannel.Play();
    }

    void Ch1PlayOneShot(AudioClip clip)
    {
        sfxChannel1.PlayOneShot(clip);
    }

    void Ch2PlayOneShot(AudioClip clip)
    {
        sfxChannel2.PlayOneShot(clip);
    }

    public void FadeOutMusicWrapper()
    {
        StartCoroutine(FadeOutAudio(musicChannel, 1f));
    }

    public void FadeInMusicWrapper()
    {
        StartCoroutine(FadeInAudio(musicChannel, 1f));
    }

    IEnumerator ChangeMusicWrapper(AudioClip clip)
    {
        yield return FadeOutAudio(musicChannel, fadeDuration);
        musicChannel.Stop();
        musicChannel.clip = clip;
        musicChannel.Play();
        yield return FadeInAudio(musicChannel, fadeDuration);
    }

    public IEnumerator FadeOutAudio(AudioSource target, float duration)
    {
        float currentTime = 0;
        float startVol = target.volume;

        while(currentTime < duration)
        {
            target.volume = Mathf.Lerp(startVol, 0, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        target.volume = 0;
    }

    public IEnumerator FadeInAudio(AudioSource target, float duration)
    {
        float currentTime = 0;
        float startVol = target.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}
