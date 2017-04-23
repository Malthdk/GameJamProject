using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronaut : MonoBehaviour {

	public static Astronaut instance;

	public bool active;
	bool damageSwitch;
	float allowedReactionTime = 2f;

	[SerializeField]
	float damage = 0.1f;

	float timeSinceStart, timeSinceActive;

	Animator anim;
	AudioSource source;
	Rigidbody2D rig;
	SpriteRenderer sp;

	float movementSpeed = -1f, liftRate = 0.05f;
	bool moveSwitch = true, grounded, lifting, complete;

	[SerializeField]
	Transform walkBoundMin, walkBoundMax, liftBound;

	void Awake(){
		anim = GetComponent<Animator> ();
		rig = GetComponent<Rigidbody2D> ();
		source = gameObject.GetComponent<AudioSource>();
		sp = GetComponentInChildren<SpriteRenderer> ();

		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);    
		}
	} //

	void Start () {
		StartCoroutine("EventUpdate");
	} //

	IEnumerator EventUpdate () {
		while (true) {
			
			if (active) {
				timeSinceActive = Time.time - timeSinceStart;
				CheckForDamageable ();
			} else {
				Walk ();
			} // active

			// anim.SetFloat ("Blend", Mathf.Abs(movementSpeed));
			yield return null;
		}
	} //

	public void Lift(){
		lifting = true;
	}

	public void NotLifting(){
		lifting = false; 
	}
		
	void HandleLift(){
		if (!complete) {
			if (lifting) {
				if (transform.position.y >= liftBound.position.y) {
					complete = true;
				} else { 
					transform.position = new Vector2 (transform.position.x, transform.position.y + liftRate);
				}
			} // lifting
		}
	} //

	void Walk(){
		rig.velocity = new Vector2 (movementSpeed, 0f);

		if (transform.position.x <= walkBoundMin.position.x && moveSwitch) {
			moveSwitch = false;
			movementSpeed = 1f;
			sp.flipX = true;

		} else if (transform.position.x >= walkBoundMax.position.x && !moveSwitch) {
			moveSwitch = true;
			movementSpeed = -1f;
			sp.flipX = false;
		}
	} //

	void CheckForDamageable() {
		if (timeSinceActive > allowedReactionTime && damageSwitch) {
			ScoreBar.scoreDecreaseMultiplier += damage;
			damageSwitch = false;
		}
	} //

	public void SetActive() {
		EventSequenceController.activeObjectives++;
		active = true;
		complete = false;
		rig.velocity = new Vector2 (0f, 0f);
		anim.CrossFade ("Astronaut_Tell", 0f);
	} //

	public void SetInactive() {
		EventSequenceController.activeObjectives--;
		active = false;
	} //

	void OnTriggerEnter2D(Collider2D other) {
		if (other.transform.tag == "ground") {
			if (active && !complete) {
				anim.CrossFade ("Astronaut_Tell", 0f);
			}else if (active && !complete) {
				SetInactive ();
				anim.CrossFade ("Walking", 0f);
			}
		}
	} //

	void OnTriggerExit2D(Collider2D other) {
		if (other.transform.tag == "ground") {
			if (active) {
				anim.CrossFade ("Astronaut_InAir", 0f);
			}
			grounded = false;
		}
	} //
}
