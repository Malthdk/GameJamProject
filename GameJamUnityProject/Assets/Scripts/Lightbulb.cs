using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightbulb : MonoBehaviour {

	public bool active;
	public int difficulty;
	bool damageable, damageSwitch;
	float allowedReactionTime = 2f;
	float tellDuration = 4f;
	float damageIncreaseRate;
	float damageIncrease;
	float damageMin, damageMax;
	float timeSinceActive;

	// Use this for initialization
	void Start () {
		StartCoroutine("EventUpdate");
	}

	// Update is called once per frame
	IEnumerator EventUpdate () {
		while (true) {
			if (active) {
				timeSinceActive += Time.deltaTime;
				CheckForDamageable ();
				Debug.Log("Active: " + active + "timeSinceActive: " + timeSinceActive);
			} else {
				timeSinceActive = 0f;
				if (damageable) {

				} else {

				}
			}
			yield return null;
		}
	} //

	void CheckForDamageable() {
		if (timeSinceActive > allowedReactionTime && damageSwitch) {
			StartCoroutine("IncreaseDamageRate");
			damageSwitch = false;
		}
	} //

	IEnumerator Tell () {
		Debug.Log("Play tell animation");
		yield return new WaitForSeconds(tellDuration);
		active = true;
	} //

	void Feedback () {
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
		StartCoroutine("Tell");
	} //

	public void SetInactive() {
		timeSinceActive = 0f;
		damageSwitch = true;
		active = false;
	} //

}
