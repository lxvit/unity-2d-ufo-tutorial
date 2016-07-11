using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {

    public float speed;

    private bool active;
    private Rigidbody2D rb2D;
    private bool moveRight;
    private Vector2 lastPosition;
    private bool isColliding;

    void Start()
    {
        active = true;
        moveRight = true;
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            return;
        Flip();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlatformStopper"))
        {
            Flip();
            Move();
        }
    }

    public void Flip()
    {
        moveRight = !moveRight;
    }

    public void Move()
    {
        if (!active)
        {
            return;
        }
        if (!moveRight)
            speed = -speed;
        Vector2 new_position = new Vector2(rb2D.position.x + speed * Time.fixedDeltaTime, rb2D.position.y);
        rb2D.MovePosition(new_position);
    }
}
