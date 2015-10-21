using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

	private ArrayList runningCoroutines;

	private Material currentMaterial;
	private Renderer rend;

	// Use this for initialization
	void Start () {
		runningCoroutines = new ArrayList ();
		rend = gameObject.GetComponent<Renderer>();
		currentMaterial = rend.material ;
		NotificationCenter.DefaultCenter.AddObserver(this, "InvisibilityTriggered");
		NotificationCenter.DefaultCenter.AddObserver(this, "BombPickedUp");
		NotificationCenter.DefaultCenter.AddObserver(this, "GooPickedUp");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void AddPowerUpCoroutineToList(Coroutine triggerCoroutine){
		runningCoroutines.Add (triggerCoroutine);
	}
	void StopRunningCoroutines(){
		foreach (Coroutine coroutine in runningCoroutines) {
			StopCoroutine(coroutine);
		}
		runningCoroutines.Clear ();
	}


	void InvisibilityTriggered(NotificationCenter.Notification notif){
		StopRunningCoroutines();
		Hashtable payload = notif.data;
		gameObject.GetComponent<Renderer>().material = (Material)payload["material"];
		Coroutine coroutine = StartCoroutine(SwitchBackMaterial((float)payload["duration"]));
		AddPowerUpCoroutineToList(coroutine);
	}

	void BombPickedUp(NotificationCenter.Notification notif){
		//the player picked up the bomb
		var inventoryController = gameObject.GetComponent<PlayerInventoryController>();
		inventoryController.bombsInInventory++;
	}

	void GooPickedUp(NotificationCenter.Notification notif){
		var inventoryController = gameObject.GetComponent<PlayerInventoryController>();
		inventoryController.goosInInventory++;
	}

	IEnumerator SwitchBackMaterial(float afterSeconds){
		yield return new WaitForSeconds(afterSeconds);
		rend.material = currentMaterial;
	}

	void OnDestroy(){
		NotificationCenter.DefaultCenter.RemoveObserver(this, "InvisibilityTriggered");
	}


}
