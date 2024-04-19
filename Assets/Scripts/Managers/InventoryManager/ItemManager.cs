using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public bool UseItem(Item item, GameObject user)
    {
        if (item.Use(user))
        {
            Debug.Log(item.name + " used successfully.");
            return true;
        }
        else
        {
            Debug.Log("Failed to use " + item.name);
            return false;
        }
    }
}
