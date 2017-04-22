using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightbulb : MonoBehaviour {

	public bool active;
	bool damageable, damageSwitch;
	float allowedReactionTime = 2f;
	float tellDuration = 3f;
	float damageIncreaseRate = 0.2f;
	float damageIncrease;
	float damageMin, damageMax;
	float timeSinceActive;

	Animator anim;

	void Awake(){
		anim = GetComponent<Animator> ();
	}

	void Start () {
		StartCoroutine("EventUpdate");
	} //
		
	IEnumerator EventUpdate () {
		while (true) {
			if (active) {
				timeSinceActive += Time.deltaTime;
				CheckForDamageable ();
				// Debug.Log("Active: " + active + "timeSinceActive: " + timeSinceActive);
			} 
			if (damageable) {
				StartCoroutine("IncreaseDamageRate");
			} else {
				damageIncrease = 0f;
			}
			yield return null;
		}
	} //

	void CheckForDamageable() {
		if (timeSinceActive > allowedReactionTime && damageSwitch) {
			damageable = true;
			StartCoroutine("IncreaseDamageRate");
			damageSwitch = false;
		}
		if (timeSinceActive < allowedReactionTime) {
			damageable = false;
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

	IEnumerator IncreaseDamageRate () {
		while (true) {
			yield return new WaitForSeconds(0.2f);
			damageIncrease += damageIncreaseRate;
			if (damageIncrease > damageMax) {
				damageIncrease = damageMax;
			}
		}
	} //

	public void SetActive() {
		Tell ();
		EventSequenceController.activeObjectives++;
	} //

	public void SetInactive() {
		EventSequenceController.activeObjectives--;
		Feedback ();
		timeSinceActive = 0f;
		damageSwitch = true;
		active = false;
	} //

}
