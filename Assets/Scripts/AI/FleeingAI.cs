using UnityEngine;
using System.Collections;

public class FleeingAI : GeneralAI {
    
	override protected void Touched(){
		Kill();
	}

	override protected void Kill(){
		DecayAndDestroy ();
	}
}
