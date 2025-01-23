using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerPrefab;

    private bool isPaused = false;

    private GameObject playerInstance;
    private Transform playerSpawnTransform;

    private void Awake()
    {
        Transform parentTransform = transform.parent;
        if(instance == null)
        {
            instance = this; 

            if(parentTransform != null)
            {
                GameObject parentGameObject = parentTransform.gameObject;
                DontDestroyOnLoad(parentGameObject);
            }
            else
            {
                Debug.LogWarning("Can't find Game Manager Object!");
            }
        }
        else
        {
            Debug.LogWarning("Game Manager Object already exists. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Game Manager is being destroyed.");
    }

    public void LoadScene(int SceneNumber)
    {
        StartCoroutine(nameof(LoadLevel), SceneNumber);
    }
    
    public IEnumerator LoadLevel(int SceneNumber)
    {
        var newScene = SceneManager.LoadSceneAsync(SceneNumber, LoadSceneMode.Single);

        while(!newScene.isDone)
        {
            yield return null;
        }

        if(UpdatePlayerSpawn())
        {
            SpawnPlayer();
        }
        else
        {
            Debug.LogWarning("Can't find player spawn location!");
        }
    }

    private bool UpdatePlayerSpawn()
    {
        bool updated = false;
        var playerSpawn = GameObject.FindGameObjectWithTag("Player Spawn");

        if(playerSpawn != null)
        {
            playerSpawnTransform = playerSpawn.transform;
            updated = true;
        }

        return updated;
    }

    public void SpawnPlayer()
    {
        if(playerInstance != null)
        {
            Destroy(playerInstance);
        }

        playerInstance = Instantiate(playerPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation);
    }

    public void Pause()
    {
        isPaused = !Time.timeScale.Equals(0f);
        Time.timeScale = isPaused ? 0f : 1f;

        Debug.Log(isPaused ? "Game Paused" : "Game Unpaused");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}
