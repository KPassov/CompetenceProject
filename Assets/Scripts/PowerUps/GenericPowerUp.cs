using UnityEngine;
using System.Collections;

public class GenericPowerUp : MonoBehaviour {

	public bool spinPowerUp = true;
	public float rotationSpeed = 45.0f;

	public float powerUpInactiveTime = 10;

	[HideInInspector] // Hides var below
	public bool powerUpActive = true;

	[HideInInspector] // Hides var below
	public Renderer rend;
	[HideInInspector] // Hides var below
	public Material currentMaterial;
	[HideInInspector] // Hides var below
	public Material inactiveMaterial;


	public void SuperStart(){
		rend = GetComponent<Renderer>();
		StartCoroutine(SpinPowerUp());

		//setting up materials
		currentMaterial = rend.material;
		inactiveMaterial =  Resources.Load("Materials/EmptyPowerUp", typeof(Material)) as Material;
	}
	// Use this for initialization
	public virtual void Start () {
		SuperStart();
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator SpinPowerUp(){
		while(spinPowerUp){
			transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
			yield return null;
		}
	}

	public virtual IEnumerator changeBackMaterial(float afterSeconds){
		yield return new WaitForSeconds(afterSeconds);
		powerUpActive = true;
		rend.material = currentMaterial;
 	}

	public virtual void OnTriggerEnter(Collider other) {
		//has picked up
		if (other.gameObject.tag.Equals("Player"))
		{
			if(powerUpActive){
				Debug.Log ("Generic OnTriggerEnter() fired!");
				powerUpActive = false;
				rend.material = inactiveMaterial;
				StartCoroutine(changeBackMaterial(powerUpInactiveTime));
			}
		}
	}
}
