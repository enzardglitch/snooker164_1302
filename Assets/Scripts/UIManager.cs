using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    private TMP_Text textScore;
    [SerializeField]
    private TMP_Text textAttempt;
    [SerializeField]
    private GameObject gameOver;

    void Awake()
    {
        instance = this;
    }
    public void UpdateScore(int score)
    {
        textScore.text = $"Score: {score}";
    }

    public void UpdateAttempt(int attempt)
    {
        textAttempt.text = $"Attempts:{attempt}/10";
    }

    public void ShowGameOver(bool show)
    {
        gameOver.SetActive(show);
    }

    public void Exit()
    {
        GameManager.instance.Exit();
    }
}
