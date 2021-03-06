﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSequenceController : MonoBehaviour {

	public static EventSequenceController instance; 

	public static int activeObjectives;

	public bool active = false;
	bool firstObjective;
	int stages; 
	public int gameTimer;
	float objectiveSequenceInterval = 3.5f;

	bool allow;

	[SerializeField]
	GameObject[] objectives;
	GameObject prevObj;

	int objectiveIdx;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);    
		}
	}

	void Start () {
		StartCoroutine ("ObjectiveControllerUpdate");
		StartCoroutine("UpdateSequenceInterval");
	}
		

	IEnumerator ObjectiveControllerUpdate () {
		while (true) {
			if (active) {
				ShuffleObjectiveArray(objectives);
				FindInactiveObjective ();	
				yield return new WaitForSeconds (objectiveSequenceInterval);
			} // if active
			yield return null;
		}
	} //

	void FindInactiveObjective(){
		for (int i = 0; i < objectives.Length; i++) {
			if (!CheckIfOjectiveIsActive (objectives [i])) {
				SetObjectiveActive (objectives [i]);
				/*if (activeObjectives != objectives.Length) {
					CheckPreviousObjective (objectives [i]);
					prevObj = objectives[i];
				} else {
					SetObjectiveActive(objectives [i]);
				}*/
				break;
			}	
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

	/*void CheckPreviousObjective(GameObject obj) {
		
		if (obj == prevObj) {
			ShuffleObjectiveArray(objectives);
			FindInactiveObjective();
		} else {
			SetObjectiveActive(obj);
		}
	}*/


	bool CheckIfOjectiveIsActive(GameObject objective){
		switch (objective.transform.tag) {
		case "bulb":
			return objective.GetComponent<Lightbulb> ().active;
			break;
		case "astronaut":
			return objective.GetComponent<Astronaut> ().active;
			break;
		case "flag":
 			return objective.GetComponent<Flag> ().active;
			break;
		case "spaceship":
			return objective.GetComponent<Spaceship>().active;
			break;
		case "tape":
			return objective.GetComponent<FilmCanvas> ().active;
			break;
		default:
			return true;
			break;
		}
	}

	/*GameObject GetPreviousObjective() {
		
	}*/
		
	IEnumerator UpdateSequenceInterval(){
		while (true) {
			if (active) {
				yield return new WaitForSeconds (1f);
				gameTimer--; 

				switch (gameTimer) {
				case 50:
					objectiveSequenceInterval = 3f;
					break;
				case 40:
					objectiveSequenceInterval = 2.5f;
					break;
				case 30:
					objectiveSequenceInterval = 2f;
					break;
				case 20:
					objectiveSequenceInterval = 1.5f;
					break;
				case 10:
					objectiveSequenceInterval = 1f;
					break;
				default:
					break;
				} // switch
			}
			yield return null;
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
			objective.GetComponent<Lightbulb> ().SetActive ();
			break;
		case "astronaut":
			objective.GetComponent<Astronaut> ().SetActive ();
			break;
		case "flag":
			objective.GetComponent<Flag> ().SetActive ();
			break;
		case "spaceship":
			objective.GetComponent<Spaceship> ().SetActive ();
			break;
		case "tape":
			objective.GetComponent<FilmCanvas> ().SetActive ();
			break;
		default:
			break;
		}
	}

	void OnTriggerEnter(){
		
	}

}
