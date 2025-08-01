using UnityEngine;

public class LoopTeleport : MonoBehaviour
{
  public enum Edge {Left, Right}
  public Edge teleportEdge;

  public float screenHalfWidth = 7.2f;
  GameObject player;
  GameObject wall;

    public void Awake()
    {
      player = GameObject.FindGameObjectWithTag("Player");
      wall = GameObject.FindGameObjectWithTag("Wall");
    }

    private void OnTriggerEnter2D(Collider2D collision){

      if(collision.CompareTag("Player") || collision.CompareTag("Wall")){
        
        Transform obj = collision.transform;
        Vector2 pos = obj.position;

        if (teleportEdge == Edge.Right){
          pos.x = -screenHalfWidth + 1f;
        }
        else if (teleportEdge == Edge.Left){
          pos.x = screenHalfWidth - 1f;
        }

        obj.position = pos;
      }
    }
}

