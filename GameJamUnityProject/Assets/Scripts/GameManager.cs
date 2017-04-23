using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


	public GameObject retryBoard, endText;
	public bool oneshot = false;

	Text lastWords;

	void Start () 
	{
		lastWords = endText.GetComponent<Text> ();
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

		yield return new WaitForSeconds(2f);

		retryBoard.SetActive(true);
		lastWords.text = "Welcome to NASA";
	}

	IEnumerator Loose()
	{
		KubrickAI.instance.looseBool = true;

		yield return new WaitForSeconds(2f);

		retryBoard.SetActive(true);
		lastWords.text = "Shit, movies are hard!";
			
	}

	public void ButtonClick(){
		StartCoroutine ("StartNextScene");
	}

	IEnumerator StartNextScene(){
		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene (4);
		
	}
}
