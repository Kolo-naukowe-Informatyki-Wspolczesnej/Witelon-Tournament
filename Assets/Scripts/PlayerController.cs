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

    public void MoveRight()
    {
        if (!Physics2D.OverlapCircle(movePoint.position +  new Vector3(1, 0, 0), .2f, whatBlocksMovement))
        {
            movePoint.position += new Vector3(1, 0, 0);
        }
    }

    public void MoveLeft()
    {
        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(-1, 0, 0), .2f, whatBlocksMovement))
        {
            movePoint.position += new Vector3(-1, 0, 0);
        }
    }

    public void MoveUp()
    {
        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, 1, 0), .2f, whatBlocksMovement))
        {
            movePoint.position += new Vector3(0, 1, 0);
        }
    }

    public void MoveDown() 
    {
        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, -1, 0), .2f, whatBlocksMovement))
        {
            movePoint.position += new Vector3(0, -1, 0);
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
    }

}
