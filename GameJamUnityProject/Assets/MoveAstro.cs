using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAstro : MonoBehaviour {

	float movementspeed = -1f;

	Rigidbody2D rig;
	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		rig.velocity = new Vector2 (movementspeed, 0f);
	}
}
