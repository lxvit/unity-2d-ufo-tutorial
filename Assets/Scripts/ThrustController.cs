using UnityEngine;
using System.Collections;

public class ThrustController : MonoBehaviour {

    private ParticleSystem exhaust;

    // Use this for initialization
    void Start () {
        exhaust = gameObject.GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            float angle = Mathf.Atan2(moveVertical, moveHorizontal) * Mathf.Rad2Deg;
            exhaust.transform.rotation = Quaternion.LookRotation(new Vector3(moveHorizontal * -1, moveVertical * -1, 0));
            exhaust.Play();
        }
        else
        {
            exhaust.Stop();
            exhaust.Clear();
        }
    }
}
