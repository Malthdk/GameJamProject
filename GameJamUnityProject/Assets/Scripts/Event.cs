using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour {

	public bool active;
	bool damageable, damageSwitch;
	int difficulty;
	float allowedReactionTime = 2f;
	float tellDuration = 4f;
	float damageIncreaseRate;
	float damageIncrease;
	float damageMin, damageMax;
	float timeSinceActive;

	// Use this for initialization
	void Start () {
		StartCoroutine("Tell");
	}
	
	// Update is called once per frame
	IEnumerator EventUpdate () {
		while (true) {
			if (active) {
				timeSinceActive += Time.deltaTime;
				CheckForDamageable ();
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
		StartCoroutine("EventUpdate");
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
		active = true;
	} //

	public void SetInactive() {
		timeSinceActive = 0f;
		damageSwitch = true;
		active = false;
	} //


}
