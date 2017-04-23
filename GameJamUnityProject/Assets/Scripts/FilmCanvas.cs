using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmCanvas : MonoBehaviour {

	public bool active;
	bool damageSwitch;
	float allowedReactionTime = 2f, tellDuration = 3f;

	[SerializeField]
	float damage = 0.1f;

	float timeSinceStart, timeSinceActive;

	Animator anim;
	AudioSource source;
	public AudioClip canvasFalling;
	public AudioClip canvasFixing;

	void Awake(){
		source = gameObject.GetComponent<AudioSource>();
		anim = GetComponent<Animator> ();
	}

	void Start () {
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
		float rnPitch = Random.Range (0.9f, 1.1f);
		source.pitch = rnPitch;
		float rnVol = Random.Range(0.8f, 1f);
		source.PlayOneShot (canvasFalling, rnVol);
		yield return new WaitForSeconds(0.5f);
		anim.SetBool ("active", true);
		active = true;
		timeSinceStart = Time.time;
	} //

	void Feedback () {
		anim.SetBool ("active", false);
		float rnVol = Random.Range(0.8f, 1f);
		source.PlayOneShot (canvasFixing, rnVol);
	} //

	public void SetActive() {
		Debug.Log("CANVAS ACTIVE");
		damageSwitch = true;
		StartCoroutine("Tell");
		EventSequenceController.activeObjectives++;
	} //

	public void SetInactive() {
		EventSequenceController.activeObjectives--;
		Feedback ();
		ScoreBar.scoreDecreaseMultiplier -= damage;
		damageSwitch = true;
		active = false;
	} //

}
