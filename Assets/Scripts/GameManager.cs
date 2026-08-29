using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int playerScore;
    public int PlayerScore { get { return playerScore; } set { playerScore = value; } }
    public int BallScore { get; set; }

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            xInput = -0.1f;
        }
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            xInput = 0.1f;
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
        Ball b = obj.GetComponent<Ball>();
        b.SetColorAndPoint(col);
        

    }

    private void ShootBall()
    {
        ballLine.SetActive(false);
        CameraBehindBall();

        Rigidbody rd = cueball.GetComponent<Rigidbody>();
        if (rd.linearVelocity.magnitude >= 0.1f)
        {
            return;
        }
        rd.AddRelativeForce(Vector3.forward*50, ForceMode.Impulse);

        rotateReset = false;
        cam.transform.parent = null;
        cam.transform.position = new Vector3(0, 30f, -42f);
        cam.transform.eulerAngles = new Vector3(45f, 0f, 0f);
        
    }

    private void RotateBall()
    {
        if (cueball != null)
        {
            cueball.transform.Rotate(new Vector3(0f, xInput, 0f));
        }
    }

    private bool rotateReset = false;
    private bool isMoving = false;

    private void StopBall()
    {
        Rigidbody rd = cueball.GetComponent<Rigidbody>();
        

        //if ((rd.linearVelocity.magnitude <= 0.01f )&& (rotateReset == false))
        {
            rotateReset = true;
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

    public void UpdateScore()
    {
        GuiScore.text = "Score: " + PlayerScore.ToString();
    }

    public void SaveGame()
    {
        StopBall();

        if (cueball != null)
        {


            PlayerPrefs.SetFloat("cueBallPosX", cueball.transform.position.x);
            PlayerPrefs.SetFloat("cueBallPosY", cueball.transform.position.y);
            PlayerPrefs.SetFloat("cueBallPosZ", cueball.transform.position.z);
            Debug.Log("Saved");
        }
        
    }

    public void LoadGame()
    {
        StopBall();
        if (cueball != null)
        {
            float x = PlayerPrefs.GetFloat("cueBallPosX", 0f);
            float y = PlayerPrefs.GetFloat("cueBallPosY", 0f);
            float z = PlayerPrefs.GetFloat("cueBallPosZ", 0f);
            cueball.transform.position = new Vector3(x, y, z);
            Debug.Log("Loaded");
        }
    }   

    private void CreateGame()
    {

    }

    private void EndGame()
    {
        Time.timeScale = 0f;
    }
}



