using UnityEngine;
using System.Collections;

public class TerroristBombController : MonoBehaviour {

	float countdownTime = 0.0f;

	private GameObject grenade;
	private GameObject explosion;

	public AudioSource beep1;
	public AudioSource beep2;
	public AudioSource beep3;
	public AudioSource beep4;
	public AudioSource beep5;
	public AudioSource explode;
	public AudioSource getout;
	public AudioSource disarm;

	AudioSource[] sequence;

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

		sequence = new AudioSource[]{beep1, beep1, beep1, beep2, beep2, beep2, beep3, beep3, beep4, beep4, beep5, beep5};

		for (int i = 0; i < sequence.Length; i++) {
			countdownTime += sequence[i].clip.length;
		}


		getout.PlayScheduled (AudioSettings.dspTime + 15.0f);

		StartCoroutine (Beep ());
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
		GetComponent<SphereCollider> ().enabled = false;
		Hashtable payload = new Hashtable();
		payload["explosion"] = explosion;
		StartCoroutine (DestroyAfter(3.0f));

		NotificationCenter.DefaultCenter.PostNotification(this, "Explode",payload);
		GameObject.FindGameObjectWithTag ("GUI").GetComponentInChildren<GUIScript> ().IncreaseHighScore (-100);
	}

	IEnumerator Beep(){
		for (int i = 0; i < sequence.Length; i++) {
			sequence[i].Play();
			yield return new WaitForSeconds(sequence[i].clip.length);

		}
		explode.Play ();
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			Disarm();
		}
	}

	void Disarm(){
		StopAllCoroutines ();
		foreach (AudioSource s in sequence) {
			s.Stop ();
		}
		disarm.Play ();
		GameObject.FindGameObjectWithTag ("GUI").GetComponentInChildren<GUIScript> ().IncreaseHighScore (100);
		GetComponent<SphereCollider> ().enabled = false;
		grenade.GetComponentInChildren<ParticleSystem> ().Stop();
		StartCoroutine (DestroyAfter(3.0f));
	}

	IEnumerator DestroyAfter(float seconds){
		yield return new WaitForSeconds (seconds);
		Destroy (gameObject);
	}

}
