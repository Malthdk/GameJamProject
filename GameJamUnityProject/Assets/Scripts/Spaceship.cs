using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour {

	public static Spaceship instance;

	public bool active;
	bool damageSwitch;
	float allowedReactionTime = 2f, tellDuration = 3f;

	float timeSinceStart, timeSinceActive;

	[SerializeField]
	float damage = 0.1f;

	Animator anim;
	AudioSource source;
	public AudioClip deflatingSound;
	public AudioClip inflatingSound;
	public AudioClip finishSound;

	float animRate;

	void Awake(){
		anim = GetComponent<Animator> ();
		source = GetComponent<AudioSource> ();

		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);    
		}
	}

	void Start () {
		StartCoroutine ("Pump");
	} //
		
	public void SetActive() {
		anim.SetBool ("active", true);
		float rnPitch = Random.Range (0.9f, 1.1f);
		source.pitch = rnPitch;
		float rnVol = Random.Range(0.8f, 1f);
		source.PlayOneShot (deflatingSound, rnVol);
		active = true;
		timeSinceStart = Time.time;
		EventSequenceController.activeObjectives++;

	} //

	IEnumerator Pump(){
		while (true) {
			if (active) {
				
				animRate += 0.1f;
				if (animRate > 0.9f) {
					animRate = 0.9f;
				}

				anim.Play ("Active", 0, animRate);

				if(animRate > 0.7f && damageSwitch){
					ScoreBar.scoreDecreaseMultiplier += damage;
					damageSwitch = false;
				} else if (animRate < 0.7f && !damageSwitch) {
					ScoreBar.scoreDecreaseMultiplier -= damage;
					damageSwitch = true;
				}

			}
			yield return new WaitForSeconds(0.3f);
		} // while
	} //

	public void Inflate(){
		Debug.Log(animRate);
		if (animRate >= 0.15f) {
			animRate -= 0.15f;
		} else {
			animRate = 0.0f;
			active = false;
		}
		anim.Play ("Active", 0, animRate);
		float rnVol = Random.Range(0.8f, 1f);
		source.PlayOneShot (inflatingSound, 0.2f);
		if (animRate == 0.0f) {
			source.PlayOneShot (finishSound, rnVol);
			active = false;
			anim.SetBool ("active", false);
			EventSequenceController.activeObjectives--;
		}

	} //
}
