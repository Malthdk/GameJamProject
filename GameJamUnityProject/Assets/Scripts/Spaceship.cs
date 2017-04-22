using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour {

	public bool active;
	bool damageSwitch;
	float allowedReactionTime = 2f, tellDuration = 3f;

	float timeSinceStart, timeSinceActive;

	[SerializeField]
	float damage = 0.1f;

	Animator anim;

	void Awake(){
		anim = GetComponent<Animator> ();
	}

	void Start () {
		StartCoroutine("EventUpdate");
		float ran = Random.Range (6f,10f);
		Invoke ("SetActive", ran);
	} //

	IEnumerator EventUpdate () {
		while (true) {
			if (active) {
				timeSinceActive = Time.time - timeSinceStart;
				CheckForDamageable ();
			}
			yield return null;
		}
	} //

	void CheckForDamageable() {
		if (timeSinceActive > allowedReactionTime && damageSwitch) {
			ScoreBar.scoreDecreaseMultiplier += damage;
			damageSwitch = false;
		} else if (timeSinceActive < allowedReactionTime && !damageSwitch){
			ScoreBar.scoreDecreaseMultiplier -= damage;
			damageSwitch = true;
		}
	} //

	void Tell(){
		Debug.Log("Play tell animation");
		anim.SetBool ("active", true);
		active = true;
	} //

	void Feedback () {
		anim.SetBool ("active", false);
		Debug.Log("Play feedback sound/animation"); 
	} //
		

	void SetActive() {
		active = false;
		timeSinceStart = Time.time;
	} //

	public void Inflate(){
		timeSinceActive = 0f;
	}

	void Deflate(){
		timeSinceStart = Time.time;
		damageSwitch = true;
		// deflate
	}

	public void SetInactive() {
		active = false;
	} //

}
