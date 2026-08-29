using UnityEngine;

public class Hit : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Hit hit = other.gameObject.GetComponent<Hit>();
        if (hit != null)
        {
            AudioManager.instance.PlaySFX(1);
        }
    }

}
