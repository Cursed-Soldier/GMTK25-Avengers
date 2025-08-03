using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartController : MonoBehaviour
{
    [HideInInspector]
    public Vector3 spawnPoint;

    void Awake()
    {
        
        if (FindObjectsOfType<RestartController>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            spawnPoint = player.transform.position;


        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        /*GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            spawnPoint = player.transform.position;*/
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = spawnPoint;
        }
    }
}
