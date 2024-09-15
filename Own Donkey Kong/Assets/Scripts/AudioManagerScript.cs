using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    [SerializeField] private AudioSource soundFXObject;
    public static AudioManagerScript Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySoundFxClip(AudioClip audioClip, Transform soundFxTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, soundFxTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioClip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlaySoundFxClip(AudioClip audioClip, Transform soundFxTransform, float volume, string audioSourceTag)
    {
        AudioSource audioSource = Instantiate(soundFXObject, soundFxTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioClip.length;

        audioSource.gameObject.tag = audioSourceTag;


        Destroy(audioSource.gameObject, clipLength);
    }

    /**
     * audio clip: the clip to play
     * transform: the location of where to spawn in the soundFx clip
     * length: length of time in seconds to play the audio clip for
     */
    public void PlaySoundFxClip(AudioClip audioClip, Transform soundFxTransform, float volume, float length)
    {
        AudioSource audioSource = Instantiate(soundFXObject, soundFxTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        Destroy(audioSource.gameObject, length);
    }

    public void PlaySoundFxClip(AudioClip audioClip, Transform soundFxTransform, float volume, float length,
        string audioSourceTag)
    {
        AudioSource audioSource = Instantiate(soundFXObject, soundFxTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        audioSource.gameObject.tag = audioSourceTag;

        Destroy(audioSource.gameObject, length);
    }

    public AudioSource CreateSoundFxAudioSource(AudioClip audioClip, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.Play();
        audioSource.enabled = true;

        return audioSource;
    }
}