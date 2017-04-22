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
	public Image lightBulbImage;
	public GameObject[] lightBulbs;

	RaycastHit2D hit;

	void Start () 
	{
		lightBulbs = GameObject.FindGameObjectsWithTag("bulb");
		Debug.Log(lightBulbs.Length);
	}

	void Update () {
		mouseClick = Input.GetMouseButtonDown(0);
		mousHold = Input.GetMouseButton(0);
		mouseRelease = Input.GetMouseButtonUp(0);
		hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if (mouseClick){
			Debug.Log("clicked mouse");

			if (hit) {
				ItemClicked(hit.transform.tag);
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
			if (mousHold)	{
				lightBulbImage.enabled = true;
			}
			else if (mouseRelease)	{
				lightBulbImage.enabled = false;
			}
		}

		else if (tag == "rope")	{
			Debug.Log("Zero Grav engaged");
		}
		else if (tag == "pump")	{
			Debug.Log("Dooont you know pump it up!");
		}
		else if (tag == "flag") {
			Debug.Log("AMMMEEEEERICA! FUCK YEAH!");
		}
		else if (tag == "tape")	{
			Debug.Log("You hit da tape man!");
		}
	}

	public void Release() {
		switch (hit.transform.tag) {
		case "bulb":
			if (hit.transform.GetComponent<Lightbulb>().active) {
				Debug.Log ("Sets bulb to inactive");
				hit.transform.GetComponent<Lightbulb> ().SetInactive ();
				lightBulbImage.enabled = false;
			}else {
				Debug.Log ("wrong placement");
				lightBulbImage.enabled = false;
			}
			break;
		default:
			break;
		}
	}
}
