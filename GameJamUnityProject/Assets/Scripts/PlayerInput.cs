using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour {

	//Input bools
	bool mouseClick;
	bool mousHold;
	bool mouseRelease;

	//Lightbulb
	public Image lightBulb;

	void Start () 
	{
		
	}

	void Update () {
		mouseClick = Input.GetMouseButtonDown(0);
		mousHold = Input.GetMouseButton(0);
		mouseRelease = Input.GetMouseButtonUp(0);

		if (mouseClick){
			Debug.Log("clicked mouse");

			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			if (hit)	{
				ItemClicked(hit.transform.gameObject.tag);
			}
			else  {
				Debug.Log("No hit");
			}
		} 
		if (mousHold) {
			Debug.Log("held mouse");
		}

		if (mouseRelease) {
			Release();
		}
	}


	public void ItemClicked (string tag) {
		if (tag == "newBulb") {
			Debug.Log("new lightbulb acquired");

			if (mousHold)
			{
				lightBulb.enabled = true;
			}
			else if (mouseRelease)
			{
				lightBulb.enabled = false;
			}
		}
		else if (tag == "rope")
		{
			Debug.Log("Zero Grav engaged");
		}
		else if (tag == "pump")
		{
			Debug.Log("Dooont you know pump it up!");
		}
		else if (tag == "flag")
		{
			Debug.Log("AMMMEEEEERICA! FUCK YEAH!");
		}
		else if (tag == "tape")
		{
			Debug.Log("You hit da tape man!");
		}
	}

	public void Release() {
		Debug.Log("RELEASE");
		if ( lightBulb.enabled == true)
		{
			lightBulb.enabled = false;
		}
	}
}
