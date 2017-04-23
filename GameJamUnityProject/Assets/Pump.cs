using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pump : MonoBehaviour {

	Animator anim;

	public static Pump instance;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);    
		}

		anim = GetComponent<Animator> ();

	} //
	
	// Update is called once per frame
	public void PumpAnim() {
		anim.CrossFade ("Pump", 0f);
	}
}
