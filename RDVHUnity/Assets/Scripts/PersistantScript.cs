using UnityEngine;

public class PersistantScript : MonoBehaviour
{
    public static PersistantScript instance;
    public bool AlreadyBegin;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    
}
