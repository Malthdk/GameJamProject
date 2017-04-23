using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

	public static Rope instance;

	float rate, increament = 0.05f;

	bool pull;

	Animator anim;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);    
		}

		anim = GetComponent<Animator> ();
	} //

	void Start(){
		StartCoroutine ("RopeUpdate");
	}

	IEnumerator RopeUpdate () {
		while (true) {
			if (pull) {
				rate += increament;
			} else {
				rate -= increament;
			}

			if (rate < 0f) {
				rate = 0f;
			} else if (rate > 1f) {
				rate = 1f;
			}

//			Debug.Log (rate);
				
			anim.Play ("PulldownRopeAnim", 0, rate);
			yield return new WaitForSeconds(0.05f);
		}
	}

	public void Pull(){
		pull = true;
		Debug.Log ("Pull");
	}

	public void Release(){
		pull = false;
		Debug.Log ("Pull");
	}
}
