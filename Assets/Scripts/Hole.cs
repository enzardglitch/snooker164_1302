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
            GameManager.instance.EndGame();
            return;
        }

        PlayerPrefs.SetInt($"{(int)b.color}Alive", 0);
        GameManager.instance.AddScore(b.Point);
        AudioManager.instance.PlaySFX(2);
        Destroy(b.gameObject);
    }
}
