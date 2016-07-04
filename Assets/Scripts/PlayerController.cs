using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float hp;

    private Rigidbody2D rb2d;
    private int count;
    private int pickupsCount;
    private bool disabledControls;

    private Text scoretText;
    private Text hpText;

    void Start()
    {
        pickupsCount = GameObject.FindGameObjectsWithTag("PickUp").Length;
        rb2d = GetComponent<Rigidbody2D>();
        scoretText = GameObject.Find("ScoreText").GetComponent<Text>();
        hpText = GameObject.Find("HpText").GetComponent<Text>();
        updateCountText();
        hpText.text = "HP: " + hp.ToString();
        count = 0;
        disabledControls = false;
    }
    
    void FixedUpdate()
    {
        if (disabledControls)
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
            count++;
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
        scoretText.text = "Pickups: " + count.ToString() + "/" + pickupsCount;
        if (count == pickupsCount) 
        {
            disablePlayer();
            GameManager gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
            gameManager.SaveHighScore();
            gameManager.ShowOverlay("You win!", true);
        }
    }

    public void disablePlayer()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0f;
        disabledControls = true;
    }

    void TakeDamage(float amount)
    {
        hp = hp - amount;
        hpText.text = "HP: " + hp.ToString();
        if (hp < 0)
        {
            disablePlayer();
            GameManager gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
            gameManager.ShowOverlay("You Lose!", false);
        }
    }
}
