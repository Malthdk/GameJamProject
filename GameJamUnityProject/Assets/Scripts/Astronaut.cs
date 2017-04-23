using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronaut : MonoBehaviour {

	public static Astronaut instance;

	public bool active;
	bool damageSwitch;

	[SerializeField]
	float damage = 0.1f, allowedReactionTime = 2f;

	float timeSinceStart, timeSinceActive;

	Animator anim;
	AudioSource mainSource;
	Rigidbody2D rig;
	SpriteRenderer sp;

	AudioSource walkSource;
	AudioSource pullSource;
	AudioSource jumpSource;

	public AudioClip liftingSound;
	public AudioClip walkingSound;
	public AudioClip landSound;
	public AudioClip tellSound;

	float movementSpeed = -1f, liftRate = 0.05f;
	bool moveSwitch = true, grounded, lifting, complete;

	[SerializeField]
	Transform walkBoundMin, walkBoundMax, liftBound;

	void Awake(){
		anim = GetComponentInChildren<Animator> ();
		rig = GetComponent<Rigidbody2D> ();
		mainSource = GetComponent<AudioSource>();
		sp = GetComponentInChildren<SpriteRenderer> ();

		walkSource = transform.GetChild(2).transform.GetComponent<AudioSource>();
		pullSource = transform.GetChild(3).transform.GetComponent<AudioSource>();
		jumpSource = transform.GetChild(4).transform.GetComponent<AudioSource>();

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
				HandleLift ();
			} else {
				Walk ();
			} // active

			// anim.SetFloat ("Blend", Mathf.Abs(movementSpeed));
			yield return null;
		}
	} //

	public void Lift(){
		float rnVol = Random.Range(0.8f, 1f);
		mainSource.PlayOneShot (liftingSound, rnVol);
		lifting = true;
	}

	public void NotLifting(){
		lifting = false; 
	}
		
	void HandleLift(){
		if (!complete) {
			if (lifting) {
				rig.gravityScale = 0f;
				if (transform.position.y >= liftBound.position.y) {
					complete = true;
					rig.gravityScale = 1f;
				} else {
					transform.position = new Vector2 (transform.position.x, transform.position.y + liftRate);
				}
			} else {
				rig.gravityScale = 1f;
			} // lifting
		}
	} //

	void Walk(){
		rig.velocity = new Vector2 (movementSpeed, 0f);

		if (!walkSource.isPlaying)
			walkSource.PlayOneShot(walkingSound, 0.8f);

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
		jumpSource.PlayOneShot(tellSound, 0.8f);
	} //

	public void SetInactive() {
		EventSequenceController.activeObjectives--;
		active = false;
	} //

	void OnTriggerStay2D(Collider2D other) {
		if (other.transform.tag == "ground") {
			if (active && !complete) {
				anim.CrossFade ("Astronaut_Tell", 0f, 0);
				walkSource.Pause();
			} else if (active && complete) {
				SetInactive ();
				anim.CrossFade ("Walking", 0f, 0);
			}
			grounded = true;
		}
	} //

	void OnTriggerEnter2D(Collider2D other) {
		if (other.transform.tag == "ground") {
			mainSource.PlayOneShot(landSound, 0.8f);
		}
	} //

	void OnTriggerExit2D(Collider2D other) {
		if (other.transform.tag == "ground") {
			if (active) {
				anim.CrossFade ("Astronaut_InAir", 0f, 0);
			}
			grounded = false;
		}
	} //
}
