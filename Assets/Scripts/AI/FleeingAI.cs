using UnityEngine;
using System.Collections;

public class FleeingAI : GeneralAI {

	override protected void VeryCloseMove(){
		moveDirection = transform.position + Vector3.Normalize(transform.position - player.transform.position) * 3f;
		navAgent.destination = new Vector3(moveDirection.x, 0, moveDirection.z);
		target = navAgent.destination;
	}
	
	override protected void CloseMove(){
		if(nearTarget(target, transform.position)){
			moveDirection = transform.position + Vector3.Normalize(transform.position - player.transform.position) * 3f;
			navAgent.destination = new Vector3(moveDirection.x, 0, moveDirection.z);
			target = navAgent.destination;
		}
	}

	override protected void IdleMove(){

	}

	override protected void Touched(){

	}
}
