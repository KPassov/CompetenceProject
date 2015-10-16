using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour {

	public float countdownTime = 3.0f;

	private GameObject grenade;
	private GameObject explosion;

	// Use this for initialization
	void Start () {
		foreach(Transform child in transform){
			if(child.CompareTag("Grenade"))
				grenade = child.gameObject;
			if(child.CompareTag("Explosion"))
				explosion = child.gameObject;
		}
		grenade.SetActive(true);
		explosion.SetActive(false);
		StartCoroutine(StartCountDown());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator StartCountDown(){
		yield return new WaitForSeconds(countdownTime);
		Explode();
	}

	void Explode(){
		grenade.SetActive(false);
		explosion.SetActive(true);

		Hashtable payload = new Hashtable();
		payload["explosion"] = explosion;

		NotificationCenter.DefaultCenter.PostNotification(this, "Explode",payload);
	}


}
