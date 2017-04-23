using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	Text timer;

	void Start () 
	{
		timer = gameObject.GetComponent<Text>();
	}
	

	void Update () 
	{
		timer.text = EventSequenceController.instance.gameTimer.ToString();
	}
}
