using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float hp;

    private Rigidbody2D rb2d;

    void Start()
    {
        GameVariables.currentHp = hp;
        rb2d = GetComponent<Rigidbody2D>();
        updateCountText();
    }
    
    void FixedUpdate()
    {
        if (GameVariables.currentState != GameVariables.StateType.PLAYING)
        {
            return;
        }
#if UNITY_STANDALONE || UNITY_WEBPLAYER
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        float moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float moveVertical = CrossPlatformInputManager.GetAxis("Vertical");
#endif
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            GameVariables.currentPickups++;
            updateCountText();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Platform"))
        {
            TakeDamage(other.relativeVelocity.magnitude);
        }
    }

    void updateCountText()
    {
        if (GameVariables.currentPickups == GameVariables.totalPickups) 
        {
            disablePlayer();
            GameVariables.currentState = GameVariables.StateType.WIN;
        }
    }

    public void disablePlayer()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0f;
    }

    void TakeDamage(float amount)
    {
        GameVariables.currentHp = GameVariables.currentHp - amount;
        if (GameVariables.currentHp < 0)
        {
            disablePlayer();
            GameVariables.currentState = GameVariables.StateType.LOST;
        }
    }
}
