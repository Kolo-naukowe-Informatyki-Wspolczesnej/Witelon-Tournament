using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool HasReachedFinish;
    public Transform movePoint;
    public LayerMask whatBlocksMovement;
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
        if (!Physics2D.OverlapCircle(movePoint.position + vector, .2f, whatBlocksMovement))
        {
            movePoint.position += vector;
        }

    }

    //public void MoveRight()
    //{
    //    if (!Physics2D.OverlapCircle(movePoint.position +  new Vector3(1, 0, 0), .2f, whatBlocksMovement))
    //    {
    //        movePoint.position += new Vector3(1, 0, 0);
    //    }
    //}

    //public void MoveLeft()
    //{
    //    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(-1, 0, 0), .2f, whatBlocksMovement))
    //    {
    //        movePoint.position += new Vector3(-1, 0, 0);
    //    }
    //}

    //public void MoveUp()
    //{
    //    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, 1, 0), .2f, whatBlocksMovement))
    //    {
    //        movePoint.position += new Vector3(0, 1, 0);
    //    }
    //}

    //public void MoveDown() 
    //{
    //    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, -1, 0), .2f, whatBlocksMovement))
    //    {
    //        movePoint.position += new Vector3(0, -1, 0);
    //    }
    //}

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        
    }



}