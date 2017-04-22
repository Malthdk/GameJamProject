using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightbulb : MonoBehaviour {

	public bool active;
	bool damageSwitch;
	float allowedReactionTime = 2f, tellDuration = 3f;

	[SerializeField]
	float damage = 0.1f;

	float timeSinceStart, timeSinceActive;

	Light pointLight;
	float startingLightValue;
	Animator anim;
	AudioSource source;
	public AudioClip breakingSound;

	public ParticleSystem pSystemExplode;

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
		}
	} //

	IEnumerator Tell(){
		float rnPitch = Random.Range(0.9f, 1.1f);
		float rnVol = Random.Range(0.8f, 1f);
		// source.pitch = rnPitch;
		source.PlayOneShot (breakingSound, rnVol);

		yield return new WaitForSeconds(0.5f);
		anim.SetBool ("active", true);
		pSystemExplode.Play();
		yield return new WaitForSeconds(0.15f);
		active = true;
		timeSinceStart = Time.time;
		yield return new WaitForSeconds(0.35f);
		pointLight.intensity = 0f;
	} //
		
	void Feedback () {
		anim.SetBool ("active", false);
		Debug.Log("Play feedback sound/animation"); 
	} //

	public void SetActive() {
		damageSwitch = true;
		StartCoroutine("Tell");
		EventSequenceController.activeObjectives++;
	} //

	public void SetInactive() {
		EventSequenceController.activeObjectives--;
		Feedback ();
		ScoreBar.scoreDecreaseMultiplier -= damage;
		damageSwitch = true;
		pointLight.intensity = startingLightValue;
		damageSwitch = true;
		active = false;
	} //

}
