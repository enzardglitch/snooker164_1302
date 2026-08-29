using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Ball b = other.GetComponent<Ball>();
        if (b == null)
        {
            return;
        }

        if (b.Point ==0)
        {
            GameManager.instance.GuiScore.text = "IDIOT";
            return;
        }
        GameManager.instance.PlayerScore += b.Point;
        GameManager.instance.UpdateScore();
        AudioManager.instance.PlaySFX(2);
        Destroy(b.gameObject);
    }
}
