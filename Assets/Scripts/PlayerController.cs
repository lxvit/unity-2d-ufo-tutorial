using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    
    private Rigidbody2D rb2d;
    private int count;
    private int pickupsCount;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        updateCountText();
        winText.text = "";
        pickupsCount = GameObject.FindGameObjectsWithTag("PickUp").Length;
    }
    
    void FixedUpdate()
    {
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

    void updateCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count == pickupsCount) 
        {
            winText.text = "You win!";
        }
    }
}
