using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public float maxImpactSpeed;

    private Rigidbody2D rb2d;
    private int count;
    private int pickupsCount;
    private bool disabledControls;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        updateCountText();
        winText.text = "";
        pickupsCount = GameObject.FindGameObjectsWithTag("PickUp").Length;
        disabledControls = false;
    }
    
    void FixedUpdate()
    {
        if (disabledControls)
        {
            return;
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            updateCountText();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            if (other.relativeVelocity.magnitude > maxImpactSpeed)
            {
                winText.text = "You lose!";
                disabledControls = true;
            }
        }
    }

    void updateCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count == pickupsCount) 
        {
            winText.text = "You win!";
        }
    }

    float calculateImpactForce(Collision2D contact)
    {
        Rigidbody2D contact_rb2d = contact.gameObject.GetComponent<Rigidbody2D>();
        var impactVelocityX = rb2d.velocity.x - contact_rb2d.velocity.x;
        impactVelocityX *= Mathf.Sign(impactVelocityX);
        var impactVelocityY = rb2d.velocity.y - contact_rb2d.velocity.y;
        impactVelocityY *= Mathf.Sign(impactVelocityY);
        var impactVelocity = impactVelocityX + impactVelocityY;
        var impactForce = impactVelocity * rb2d.mass * contact_rb2d.mass;
        impactForce *= Mathf.Sign(impactForce);
        return impactForce;
    }
}
