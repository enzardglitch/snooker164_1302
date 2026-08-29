using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] bgm;

    [SerializeField]
    private AudioSource[] sfx;

    [SerializeField]
    private AudioMixer mixer; //find way to save volume

    [SerializeField]
    public static AudioManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return; // Exit early so the rest of Awake doesn't run
        }

        // Set this object as the definitive instance and protect it from scene loads
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void StopAllBGM()
    {
        foreach (AudioSource background in bgm)
        {
            background.Stop();
            
        }
    }

    public void PlayBGM(int index)
    {
        StopAllBGM();
        if (index < 0 || index >= bgm.Length)
        {
            Debug.LogError("Invalid BGM index: " + index);
            return;
        }
        bgm[index].Play();
    }

    public void PlaySFX(int index)
    {
        if (index < 0 || index >= sfx.Length)
        {
            Debug.LogError("Invalid SFX index: " + index);
            return;
        }
        sfx[index].PlayOneShot(sfx[index].clip);
    }

    public void AdjustMasterVolume(float volume)
    {
        mixer.SetFloat("master", volume);
        PlayerPrefs.SetFloat("master", volume);
        PlayerPrefs.Save();
    }

    public float LoadCurrentMasterVolume()
    {
        return PlayerPrefs.GetFloat("master", 0f); //0f is the default value if the key doesn't exist
    }
}
