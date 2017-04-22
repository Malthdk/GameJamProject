﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSequenceController : MonoBehaviour {

	public bool active;
	bool firstObjective;
	int stages; 
	int gameTimer;
	float objectiveSequenceInterval = 3.5f;

	[SerializeField]
	GameObject[] objectives;

	void Start () {
		StartCoroutine ("ObjectiveControllerUpdate");
	}
		

	IEnumerator ObjectiveControllerUpdate () {
		while (true) {
			if (active) {
				if (firstObjective) {
					StartCoroutine("UpdateSequenceInterval");
					firstObjective = false;
					int ran = Random.Range (0, objectives.Length);
					SetObjectiveActive (objectives[ran]);
					yield return new WaitForSeconds (objectiveSequenceInterval);
				}

				SetObjectiveActive (FindInactiveObejctive ());
				yield return new WaitForSeconds (objectiveSequenceInterval);
				UpdateSequenceInterval ();
			} // if active
		}
	} //

	GameObject FindInactiveObejctive(){
		ShuffleObjectiveArray (objectives);
		foreach (GameObject go in objectives) {
			if (!CheckIfOjectiveIsActive (go)) {
				return go;
				break;
			}
			Debug.Log ("FindInactiveEvent");
		}
	} //

	void ShuffleObjectiveArray(GameObject[] obj)
	{
		// Knuth shuffle algorithm :: courtesy of Wikipedia :)
		for (int t = 0; t < obj.Length; t++ )
		{
			GameObject tmp = obj[t];
			int r = Random.Range(t, obj.Length);
			obj[t] = obj[r];
			obj[r] = tmp;
		}
	} //


	bool CheckIfOjectiveIsActive(GameObject objective){
		switch (objective.tag) {
		case "bulb":
			// return objective.GetComponent<Lightbulb> ().active;
			break;
//		case "rope":
//			// return objective.GetComponent<Lightbulb> ().active;
//			break;
//		case "flag":
//			// return objective.GetComponent<Lightbulb> ().active;
//			break;
//		case "pump":
//			// return objective.GetComponent<Lightbulb> ().active;
//			break;
//		case "tape":
//			// return objective.GetComponent<Lightbulb> ().active;
//			break;
		default:
			break;
		}
	}
		
	IEnumerator UpdateSequenceInterval(){
		while (true) {
			yield return new WaitForSeconds (1f);
			gameTimer++; 

			switch (gameTimer) {
			case 10:
				objectiveSequenceInterval = 3f;
				break;
			case 20:
				objectiveSequenceInterval = 2.5f;
				break;
			case 30:
				objectiveSequenceInterval = 2f;
				break;
			case 40:
				objectiveSequenceInterval = 1.5f;
				break;
			case 50:
				objectiveSequenceInterval = 1f;
				break;
			default:
				break;
			} // switch
		}
	} //

	void Reset(){
		firstObjective = true;
		gameTimer = 0;
	} //

	void SetActive(){
		active = true;
	} //

	void SetInactive(){
		active = false;
	} //

	void SetObjectiveActive(GameObject objective){
		switch (objective.tag) {
		case "bulb":
			// objective.GetComponent<Lightbulb> ().SetActive ();
			break;
//		case "rope":
//			// objective.GetComponent<Lightbulb> ().SetActive ();
//			break;
//		case "flag":
//			// objective.GetComponent<Lightbulb> ().SetActive ();
//			break;
//		case "pump":
//			// objective.GetComponent<Lightbulb> ().SetActive ();
//			break;
//		case "tape":
//			// objective.GetComponent<Lightbulb> ().SetActive ();
//			break;
		default:
			break;
		}
	}

	void OnTriggerEnter(){
		
	}

}
