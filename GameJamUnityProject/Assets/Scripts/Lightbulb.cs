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

	Light pointLight;
	float startingLightValue;
	Animator anim;
	AudioSource source;
	public AudioClip breakingSound;

	void Awake(){
		anim = GetComponent<Animator> ();
	}

	void Start () {
		source = gameObject.GetComponent<AudioSource>();
		pointLight = gameObject.GetComponentInChildren<Light>();
		startingLightValue = pointLight.intensity;
		StartCoroutine("EventUpdate");
	} //
		
	IEnumerator EventUpdate () {
		while (true) {
			if (active) {
				timeSinceActive += Time.deltaTime;
				CheckForDamageable ();
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

	IEnumerator Tell(){
		float rnPitch = Random.Range(0.9f, 1.1f);
		float rnVol = Random.Range(0.8f, 1f);
		source.pitch = rnPitch;
		source.PlayOneShot (breakingSound, rnVol);

		yield return new WaitForSeconds(0.5f);
		anim.SetBool ("active", true);
		yield return new WaitForSeconds(0.15f);
		active = true;
		yield return new WaitForSeconds(0.35f);
		pointLight.intensity = 0f;
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
		StartCoroutine("Tell");
		EventSequenceController.activeObjectives++;
	} //

	public void SetInactive() {
		EventSequenceController.activeObjectives--;
		Feedback ();
		pointLight.intensity = startingLightValue;
		timeSinceActive = 0f;
		damageSwitch = true;
		active = false;
	} //

}
