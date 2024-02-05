using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float upForce;
    public float sideForce;

    private bool jump = false;
    private bool moveRight = false;
    private bool moveLeft = false;

    private void Update()
    {
        jump |= Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow);
        moveRight |= Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow);
        moveLeft |= Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow);
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);

        if (jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0, upForce * Time.deltaTime));
            jump = false;
        }

        if (moveRight)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.AddForce(new Vector2(sideForce * Time.deltaTime, 0f));
            moveRight = false;
        }

        if (moveLeft)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.AddForce(new Vector2(-sideForce * Time.deltaTime, 0f));
            moveLeft = false;
        }
    }
}
