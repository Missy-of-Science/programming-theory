using UnityEngine;

public class DataPersistanceManager : MonoBehaviour
{
    public static DataPersistanceManager Instance;
    public float difficulty;
    public bool isGameActive = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
