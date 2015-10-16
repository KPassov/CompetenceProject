using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    Rigidbody rb;
    public float thrust = 500f;
    public float maxSpeed = 10f;

	void Awake () {
        rb = GetComponent<Rigidbody>();	
	}
	
	void Update () {
        if(Input.GetKey("down")){
            rb.AddForce(Vector3.back * thrust);
        } 
        if(Input.GetKey("up")){
            rb.AddForce(Vector3.forward * thrust);
        }  
        if(Input.GetKey("right")){
            rb.AddForce(Vector3.right * thrust);    
        }
        if(Input.GetKey("left")){
            rb.AddForce(Vector3.left * thrust);    
        }
		if(Input.GetKeyDown("space")){
			GameObject bomb = Resources.Load("Prefabs/Bomb", typeof(GameObject)) as GameObject;
			Instantiate(bomb,transform.position,Quaternion.identity);
		}
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed),
                                  Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed),
                                  Mathf.Clamp(rb.velocity.z, -maxSpeed, maxSpeed));
	}
}
