using UnityEngine;

public class ItemSpawnerController : MonoBehaviour
{
    public bool isActivated;
    public bool CanSpawnMany = false;
    private float timeTillNextSpawn = 0;
    public float spawnBuffer = 2;

    public WallButtonTrigger wbTrigger;
    public GroundButtonTrigger gbTrigger;

    public GameObject prefabToSpawn;
    private GameObject curInstantiation;

    public GameObject spawnPoint;
    private Vector3 spawnVector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        spawnVector = spawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks for if spawner is tied to button
        if (wbTrigger != null && wbTrigger.isActive == true)
        {
            isActivated = true;
        }
        else if (gbTrigger != null && gbTrigger.isActive == true)
        {
            isActivated = true;
        }
        else if (gbTrigger != null && gbTrigger.isActive == false)
        {
            isActivated = false;
        }

        //Time to spawn a new item if it only does one
        if (curInstantiation == null && isActivated && CanSpawnMany == false)
        {
            curInstantiation = Instantiate(prefabToSpawn, spawnVector, Quaternion.identity);
        }
        else if(CanSpawnMany == true && isActivated && Time.time > timeTillNextSpawn)
        {
            Instantiate(prefabToSpawn, spawnVector, Quaternion.identity);
            timeTillNextSpawn = Time.time + spawnBuffer;
        }
    }
}
