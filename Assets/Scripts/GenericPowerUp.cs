using UnityEngine;
using System.Collections;

public class GenericPowerUp : MonoBehaviour {

	public bool spinPowerUp = true;
	public float rotationSpeed = 45.0f;

	public float powerUpDuration = 10;

	public Material powerUpMaterial;

	[HideInInspector] // Hides var below
	public Renderer rend;
	[HideInInspector] // Hides var below
	public Material currentMaterial;
	[HideInInspector] // Hides var below
	public Shader currentShader;
	[HideInInspector] // Hides var below
	public Shader powerUpShader;


	public void SuperStart(){
		rend = GetComponent<Renderer>();
		StartCoroutine(SpinPowerUp());
		currentShader = rend.material.shader;
		powerUpShader = Shader.Find("Toon/Basic Outline");
		currentMaterial = rend.material;
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

	public virtual IEnumerator changeBackMaterial(int afterSeconds){
		yield return new WaitForSeconds(afterSeconds);
		rend.material = currentMaterial;
 	}

	public virtual void OnTriggerEnter(Collider other) {
		//has picked up
		if (other.gameObject.tag.Equals("Player"))
		{
			Debug.Log ("Generic OnTriggerEnter() fired!");
		}
	}
}
