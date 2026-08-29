using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return; // Exit early so the rest of Awake doesn't run
        }

        // Set this object as the definitive instance and protect it from scene loads
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
