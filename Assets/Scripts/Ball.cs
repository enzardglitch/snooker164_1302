using UnityEngine;
using UnityEngine.EventSystems;


public enum BallColor
{
    White, //try standardizing every color into the saving/loading script, using foreach
    Red,
    Yellow,
    Green,
    Brown,
    Blue,
    Pink,
    Black,
}


public class Ball : MonoBehaviour
{
    [SerializeField]
    private int point;
    public int Point { get { return point; } set { point = value; } }

    [SerializeField]
    private MeshRenderer rd;

    [SerializeField]
    private BallColor color;



    void Awake()
    {
        rd = GetComponent<MeshRenderer>();
    }

    public void SetColorAndPoint(BallColor col)
    {
        switch (col)
        {
            case BallColor.White:
                point = 0;
                rd.material.color = Color.white;
                break;
            case BallColor.Red:
                point = 1;
                rd.material.color = Color.red;
                break;
            case BallColor.Yellow:
                point = 2;
                rd.material.color = Color.yellow;
                break;
            case BallColor.Green:
                point = 3;
                rd.material.color = Color.green;
                break;
            case BallColor.Brown:
                point = 4;
                rd.material.color = Color.brown;
                break;
            case BallColor.Blue:
                point = 5;
                rd.material.color = Color.blue;
                break;
            case BallColor.Pink:
                point = 6;
                rd.material.color = Color.pink;
                break;
            case BallColor.Black:
                point = 7;
                rd.material.color = Color.black;
                break;
        }
    }

   
}
