using UnityEngine;
using System.Collections;

public class BouncingBallController : MonoBehaviour {

    public float force;
    private Rigidbody2D myBody;

    // Use this for initialization
    void Start () {
        myBody = GetComponent<Rigidbody2D>();
        myBody.AddForce(Random.onUnitSphere * force, ForceMode2D.Impulse);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        myBody.velocity = myBody.velocity.normalized * force;
    }
}
