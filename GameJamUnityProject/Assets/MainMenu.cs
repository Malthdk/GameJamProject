using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayTutorial () {
		StartCoroutine ("LoadTutL");
	}
	
	public void PlayGame () {
		StartCoroutine ("LoadGameL");
	}

	IEnumerator LoadTutL(){
		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene (3);
	}

	IEnumerator LoadGameL(){
		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene (2);
	}
}
