using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


	public GameObject retryBoard;
	public bool oneshot = false;

	void Start () 
	{
		
	}
	

	void Update () 
	{
		if (EventSequenceController.instance.gameTimer <= 0 && !oneshot)
		{
			oneshot = true;
			StartCoroutine(Win());
		}

		if (ScoreBar.instance.score <= 0 && EventSequenceController.instance.gameTimer >= 0 && !oneshot)
		{
			oneshot = true;
			StartCoroutine(Loose());
		}
	}

	IEnumerator Win()
	{
		KubrickAI.instance.winBool = true;

		yield return new WaitForSeconds(9f);

		//Start cutscene
	}

	IEnumerator Loose()
	{
		KubrickAI.instance.looseBool = true;

		yield return new WaitForSeconds(5f);

		retryBoard.SetActive(true);
	}
}
