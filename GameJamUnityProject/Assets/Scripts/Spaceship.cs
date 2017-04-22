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

	float animRate;

	void Awake(){
		anim = GetComponent<Animator> ();

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
		active = true;
		timeSinceStart = Time.time;
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
				}else if (animRate < 0.7f && !damageSwitch) {
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

		if (animRate == 0.0f) {
			active = false;
			anim.SetBool ("active", false);
		}

	} //
}
