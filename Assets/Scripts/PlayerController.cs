using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public bool HasReachedFinish { get; set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HasReachedFinish = false;
    }

    public void MoveRight()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    public void MoveLeft()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }

    public void JumpLeft()
    {
        rb.velocity = new Vector2(-moveSpeed, moveSpeed);
    }

    public void JumpRight()
    {
        rb.velocity = new Vector2(moveSpeed, moveSpeed);
    }

    public void JumpUp()
    {
        rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
    }

    public void Wait()
    {
        rb.velocity = Vector2.zero;
    }
}
