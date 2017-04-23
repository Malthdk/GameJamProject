using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

	[SerializeField]
	GameObject counter;

	Text count;

	void Awake(){
		count = counter.GetComponent<Text> ();
	}
	void Start () {
		StartCoroutine ("Countdown");
	}
	
	IEnumerator Countdown () {
		count.text = "Ready";
		yield return new WaitForSeconds (3f);
		count.text = "5";
		yield return new WaitForSeconds (1f);
		count.text = "4";
		yield return new WaitForSeconds (1f);
		count.text = "3";
		yield return new WaitForSeconds (1f);
		count.text = "2";
		yield return new WaitForSeconds (1f);
		count.text = "1";
		yield return new WaitForSeconds (1f);
		count.text = "Action!!";
		yield return new WaitForSeconds (1f);
		EventSequenceController.instance.active = true;
		Destroy (gameObject);
	} //
}
