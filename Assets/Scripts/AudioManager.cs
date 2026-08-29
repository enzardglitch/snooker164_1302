using UnityEngine;
using UnityEngine.Audio;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StopAllBGM()
    {
        foreach (var background in bgm)
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
        bgm[index].Play(); //bgm[i].PlayDelayed(2f);
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
