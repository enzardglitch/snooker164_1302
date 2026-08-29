using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int playerScore;
    public int PlayerScore { get { return playerScore; } set { playerScore = value; } }
    public int BallScore { get; set; }

    private int playerAttempt = 0;

    [SerializeField]

    private GameObject ballGroup;

    [SerializeField]
    private GameObject[] ballPositions;

    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private GameObject cueball;

    [SerializeField]
    private GameObject ballLine;

    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private float xInput = 0f;

    [SerializeField]
    private TMP_Text guiScore;
    public TMP_Text GuiScore { get { return guiScore; } set { guiScore = value; } }

    public static GameManager instance;
    
    

    void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        Time.timeScale = 1f;
        UIManager.instance.ShowGameOver(false);


        CameraBehindBall();

        SetBall(BallColor.Red);
        SetBall(BallColor.Yellow);
        SetBall(BallColor.Green);
        SetBall(BallColor.Brown);
        SetBall(BallColor.Blue);
        SetBall(BallColor.Pink);
        SetBall(BallColor.Black);

        if (Settings.fromSave == true)
        {
            LoadGame();
        }

        UIManager.instance.UpdateScore(playerScore);
        UIManager.instance.UpdateAttempt(playerAttempt);

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ShootBall();
        }
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            StopBall();
        }
        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed )
        {
            xInput = -50f;
        }
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            xInput = 50f;
        }
        else
        {
            xInput = 0f;
        }

        if (Keyboard.current.leftShiftKey.isPressed && Keyboard.current.sKey.wasPressedThisFrame)
        {
            SaveGame();
        }

        RotateBall();
        
    }

    private void SetBall(BallColor col)
    {
        GameObject obj = Instantiate(ballPrefab,ballPositions[(int)col].transform.position, Quaternion.identity);
        obj.transform.SetParent(ballGroup.transform);
        Ball b = obj.GetComponent<Ball>();
        b.SetColorAndPoint(col);
        

    }

    private void ShootBall()
    {
        AudioManager.instance.PlaySFX(0);
        ballLine.SetActive(false);
        CameraBehindBall();

        Rigidbody rd = cueball.GetComponent<Rigidbody>();
        if (rd.linearVelocity.magnitude >= 0.1f)
        {
            return;
        }
        rd.AddRelativeForce(Vector3.forward*50, ForceMode.Impulse);

        cam.transform.parent = null;
        cam.transform.position = new Vector3(0, 30f, -42f);
        cam.transform.eulerAngles = new Vector3(45f, 0f, 0f);
        
    }

    private void RotateBall()
    {
        if (cueball != null)
        {
            cueball.transform.Rotate(new Vector3(0f, xInput*Time.deltaTime, 0f));
        }
    }

    private void StopBall()
    {
        EndAttempt();
        Rigidbody rd = cueball.GetComponent<Rigidbody>();
        

        {
            cueball.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            rd.linearVelocity = Vector3.zero;
            rd.angularVelocity = Vector3.zero;
            CameraBehindBall();
            ballLine.SetActive(true);
        }
    }

    private void CameraBehindBall()
    {
        cam.transform.parent = cueball.transform;
        cam.transform.position = cueball.transform.position + new Vector3(0f, 7f, -15f);
        cam.transform.eulerAngles = new Vector3(30f, 0f, 0f);
    }

    public void AddScore(int score)
    {
        playerScore += score;
        UIManager.instance.UpdateScore(playerScore);
    }

    private void EndAttempt()
    {
        playerAttempt++;
        UIManager.instance.UpdateAttempt(playerAttempt);
        if (playerAttempt >= 10)
        {
            EndGame();
        }
    }

    private void SaveGame()
    {

        if (cueball != null)
        {
            PlayerPrefs.SetInt("playerScore", playerScore);
            PlayerPrefs.SetInt("playerAttempt", playerAttempt);

            PlayerPrefs.SetFloat("cueBallPosX", cueball.transform.position.x);
            PlayerPrefs.SetFloat("cueBallPosY", cueball.transform.position.y);
            PlayerPrefs.SetFloat("cueBallPosZ", cueball.transform.position.z);

            foreach (Transform ball in ballGroup.transform)
            {
                BallColor ballcolor = ball.gameObject.GetComponent<Ball>().color;
                PlayerPrefs.SetFloat($"{(int)ballcolor}PosX", ball.transform.position.x);
                PlayerPrefs.SetFloat($"{(int)ballcolor}PosY", ball.transform.position.y);
                PlayerPrefs.SetFloat($"{(int)ballcolor}PosZ", ball.transform.position.z);

                PlayerPrefs.SetInt($"{(int)ballcolor}Alive", 1);
                print(ballcolor);
            }

            Debug.Log("Saved");
        }
        
    }

    private void LoadGame()
    {
        if (cueball != null)
        {
            playerScore = PlayerPrefs.GetInt("playerScore", 0);
            playerAttempt = PlayerPrefs.GetInt("playerAttempt", 0);

            float x = PlayerPrefs.GetFloat("cueBallPosX", 0f);
            float y = PlayerPrefs.GetFloat("cueBallPosY", 0f);
            float z = PlayerPrefs.GetFloat("cueBallPosZ", 0f);
            cueball.transform.position = new Vector3(x, y, z);

            foreach (Transform ball in ballGroup.transform)
            {
                BallColor ballcolor = ball.gameObject.GetComponent<Ball>().color;
                print(ballcolor);

                int alive = PlayerPrefs.GetInt($"{(int)ballcolor}Alive", 0);

                if (alive == 0)
                {
                    Destroy(ball.gameObject);
                    continue;
                }
                float bx = PlayerPrefs.GetFloat($"{(int)ballcolor}PosX", 0f);
                float by = PlayerPrefs.GetFloat($"{(int)ballcolor}PosY", 0f);
                float bz = PlayerPrefs.GetFloat($"{(int)ballcolor}PosZ", 0f);

                ball.position = new Vector3(bx, by, bz);
            }

                Debug.Log("Loaded");
        }
    }   

    public void EndGame()
    {
        playerAttempt = 10;
        Time.timeScale = 0f;
        UIManager.instance.ShowGameOver(true);
    }

    public void Exit()
    {
        if (playerAttempt < 10)
        {
            SaveGame();
        }
        
        SceneManager.LoadScene("MainMenu");
    }
}



