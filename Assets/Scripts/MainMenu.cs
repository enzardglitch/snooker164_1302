using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject adjustPanel;


    void Start()
    {
        Time.timeScale = 1f;
        AudioManager.instance.LoadCurrentMasterVolume();
        AudioManager.instance.PlayBGM(0);
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void StartNewGame()
    {
        Settings.fromSave = false;
        SceneManager.LoadScene("Loading");
    }

    public void LoadSavedGame()
    {
        Settings.fromSave = true;
        SceneManager.LoadScene("Loading");
    }




    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowHideAdjustPanel(bool show)
    {
        adjustPanel.SetActive(show);
    }

    public void SetVolume(float volume)
    {
        AudioManager.instance.AdjustMasterVolume(volume);
    }

}
