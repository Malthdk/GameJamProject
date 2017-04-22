using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour {

	//Input bools
	bool mouseClick;
	bool mousHold;
	bool mouseRelease;
	bool draggingBulb;

	//Lightbulb
	public Image lightBulbImage;
	public GameObject[] lightBulbs;

	RaycastHit2D hit;

	void Start () 
	{
		lightBulbs = GameObject.FindGameObjectsWithTag("bulb");
	}

	void Update () {
		mouseClick = Input.GetMouseButtonDown(0);
		mousHold = Input.GetMouseButton(0);
		mouseRelease = Input.GetMouseButtonUp(0);

		hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if (mouseClick) {
			if (hit) {
				ItemClicked (hit.transform.tag);
			} else {
				//s
			}
		} else if (mouseRelease) {
			Release ();
		} 
	}
		
	public void ItemClicked (string tag) {

		if (tag == "newBulb") {
			if (mousHold)	{
				lightBulbImage.enabled = true;
				draggingBulb = true;
			}
		}

		else if (tag == "rope")	{
			Debug.Log("Zero Grav engaged");
		}
		else if (tag == "pump")	{
			Spaceship.instance.Inflate();
			Debug.Log("hiiiiiiiiiiti it");
		}
		else if (tag == "flag") {
			if (hit.transform.GetComponent<Flag>().active) {
				hit.transform.GetComponent<Flag>().SetInactive();
			}
		}
		else if (tag == "tape")	{
			if (hit.transform.GetComponent<FilmCanvas>().active) {
				hit.transform.GetComponent<FilmCanvas>().SetInactive();
			}
		}
	}

	public void Release() {
		if (hit.transform.tag == "bulb") {
			if (hit.transform.GetComponent<Lightbulb>().active && draggingBulb) {
				hit.transform.GetComponent<Lightbulb> ().SetInactive ();
				lightBulbImage.enabled = false;
			} else  {
				lightBulbImage.enabled = false;
				draggingBulb = false;
			}
		} else  {
			lightBulbImage.enabled = false;
			draggingBulb = false;
		}
	} // 
}
