using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

	private Material currentMaterial;
	private Renderer rend;

	// Use this for initialization
	void Start () {
		rend = gameObject.GetComponent<Renderer>();
		currentMaterial = rend.material ;
		NotificationCenter.DefaultCenter.AddObserver(this, "InvisiblityTriggered");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InvisiblityTriggered(NotificationCenter.Notification notif){
		Hashtable payload = notif.data;
		gameObject.GetComponent<Renderer>().material = (Material)payload["material"];
		StartCoroutine(SwitchBackMaterial((float)payload["duration"]));
	}

	IEnumerator SwitchBackMaterial(float afterSeconds){
		yield return new WaitForSeconds(afterSeconds);
		rend.material = currentMaterial;
	}
	
}