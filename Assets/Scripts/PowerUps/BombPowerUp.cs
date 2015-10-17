using UnityEngine;
using System.Collections;

public class BombPowerUp : GenericPowerUp {
	

	#pragma warning disable 0114
	// Use this for initialization
	void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player"){
			if(powerUpActive){
				Hashtable payload  = new Hashtable();
				NotificationCenter.DefaultCenter.PostNotification(this, "BombedPickedUp",payload);
			}
		}
		// fire the super method
		base.OnTriggerEnter(other);
	}
	#pragma warning restore 0114
}
