using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

	public static Rope instance;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);    
		}
	} //
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Pull(){
		
	}

	public void Release(){
		
	}
}
