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
		StartCoroutine("EventUpdate");
		StartCoroutine ("Pump");
		float ran = Random.Range (6f,10f);
		Invoke ("SetActive", ran);
	} //
		
	void SetActive() {
		anim.SetBool ("active", true);
		active = true;
		timeSinceStart = Time.time;
	} //

	IEnumerator Pump(){
		while (true) {
			if (active) {
				animRate += 0.01f;
				if (animRate > 0.9f) {
					animRate = 0.9f;
				}
				if(animRate > 0.7f && damageSwitch){
					ScoreBar.scoreDecreaseMultiplier += damage;
					damageSwitch = false;
				}else if (animRate < 0.7f && !damageSwitch) {
					ScoreBar.scoreDecreaseMultiplier -= damage;
					damageSwitch = true;
				}
				anim.Play ("Active", 0, animRate);
			}
			yield return new WaitForSeconds(1f);
		} // while
	} //

	public void Inflate(){
		Debug.Log(animRate);
		if (animRate >= 0.3f) {
			animRate -= 0.3f;
		} else {
			animRate = 0.0f;
		}
		anim.Play ("Active", 0, animRate);

	} //

	public void SetInactive() {
		active = false;
	} //

}
