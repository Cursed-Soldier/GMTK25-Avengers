using UnityEngine;

public class ItemSpawnerController : MonoBehaviour
{
    public bool isActivated;
  
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

        //Time to spawn a new item
        if (curInstantiation == null && isActivated)
        {
            curInstantiation = Instantiate(prefabToSpawn, spawnVector, Quaternion.identity);
        }
    }
}
