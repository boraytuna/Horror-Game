using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    [SerializeField] private GameState currentState;
    public int totalZombiesAlive; // Track the number of zombies currently alive

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

    void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case GameState.MainMenu:
                // Handle main menu logic
                break;
            case GameState.Playing:
                EnableGameplaySystems(true);
                break;
            case GameState.Paused:
                EnableGameplaySystems(false);
                break;
            case GameState.GameOver:
                EnableGameplaySystems(false);
                SceneManager.LoadScene("GameOverScene");
                break;
        }
    }

    private void EnableGameplaySystems(bool isEnabled)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = isEnabled;
            player.GetComponent<PlayerShoot>().enabled = isEnabled;
        }
        // Toggle enemy systems based on game state
        ZombieAI[] zombies = FindObjectsOfType<ZombieAI>();
        foreach (var zombie in zombies)
        {
            zombie.enabled = isEnabled;
        }
    }

    public void PlayerDied()
    {
        ChangeState(GameState.GameOver);
    }

    public void ZombieKilled()
    {
        totalZombiesAlive--;
        if (totalZombiesAlive <= 0)
        {
            // All zombies are dead, handle level completion
            Debug.Log("All Zombies Are Killed!");
        }
    }

    public void RegisterZombie()
    {
        totalZombiesAlive++;
    }
}
