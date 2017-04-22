using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLightbulb : MonoBehaviour {

	float speed = 10000f;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, Input.mousePosition, step);
	}
}
