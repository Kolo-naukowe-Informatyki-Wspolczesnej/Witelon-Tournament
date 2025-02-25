using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool HasReachedFinish;
    public Transform movePoint;
    public LayerMask blocksMovement;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movePoint.parent = null;
        HasReachedFinish = false;
    }

    public enum Direction
    {
        UP,
        DOWN,
        RIGHT,
        LEFT
    }
    public void Move(Direction direction)
    {
        Vector3 vector;
        switch (direction)
        {
            case Direction.UP:
                vector = new Vector3(0, 1, 0);
                break;
            case Direction.DOWN:
                vector = new Vector3(0, -1, 0);
                break;
            case Direction.RIGHT:
                vector = new Vector3(1, 0, 0);
                break;
            case Direction.LEFT:
                vector = new Vector3(-1, 0, 0);
                break;
            default:
                vector = new Vector3(0, 0, 0);
                break;
        }
        if (!Physics2D.OverlapCircle(movePoint.position + vector, .2f, blocksMovement))
        {
            movePoint.position += vector;
        }
        else Debug.Log("Collision detected");

    }


    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        
    }



}