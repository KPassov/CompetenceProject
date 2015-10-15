using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    Rigidbody rb;
    public float thrust = 500f;

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
        if(Input.GetKey("left")){
            rb.AddForce(Vector3.left * thrust);    
        }  
        if(Input.GetKey("right")){
            rb.AddForce(Vector3.right * thrust);    
        }
	}
}
