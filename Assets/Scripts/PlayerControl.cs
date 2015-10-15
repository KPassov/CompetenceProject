using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    Rigidbody rb;
    public float thrust = 500f;

	void Awake () {
        rb = GetComponent<Rigidbody>();	
	}
	
	void Update () {
        if(Input.GetButton("down")){
            rb.AddForce(Vector3.backwards * thrust);
        } 
        if(Input.GetButton("up")){
            rb.AddForce(Vector3.forwards * thrust);
        }  
        if(Input.GetButton("left")){
            rb.AddForce(Vector3.left * thrust);    
        }  
        if(Input.GetButton("right")){
            rb.AddForce(Vector3.right * thrust);    
        }
	}
}
