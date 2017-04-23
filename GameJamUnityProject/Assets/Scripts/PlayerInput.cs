using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour {

	//Input bools
	bool mouseClick;
	bool mouseHold;
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
		mouseHold = Input.GetMouseButton(0);
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
		} else if (mouseHold) {
			
		}
	}
		
	public void ItemClicked (string tag) {

		if (tag == "newBulb") {
			if (mouseHold)	{
				lightBulbImage.enabled = true;
				draggingBulb = true;
			}
		}

		else if (tag == "rope")	{
			if (mouseHold) {
				Debug.Log ("hold rope");
				Astronaut.instance.Lift ();
			} else {
				Debug.Log ("releaseRope");
				Astronaut.instance.NotLifting ();
			}
		}
		else if (tag == "pump")	{
			if (hit.transform.GetComponentInParent<Spaceship>().active) {
				Spaceship.instance.Inflate();
				Pump.instance.PumpAnim ();
			}
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
			Astronaut.instance.NotLifting ();
		}
	} // 
}
