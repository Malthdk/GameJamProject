using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {
	public Image speech;
	public Text text;

	public GameObject bulbArrows, meterArrow, astroArrows, spaceshipArrows, flagArrow, canvasArrow;

	private string intro = "Alright, we're going live any minute now. Maurice couldn't be here today so you will have to. Have you ever been on a set before?";
	private string intro2 = "Agrh.. don't answer that. Of course you havn't, you're as young as my daughters. Let's make sure you know what to do.";
	private string intro3 = "You see that huge meter over there? Thanks to our friends at NASA, that will tell us wether the world believes in us. Do I even have to say it? Keep it in the green.";
	private string intro4 = "We have to make them believe that we were on the moon, okay? Lets begin.";

	int order;

	// Use this for initialization
	void Start () {
		text.text = intro;

		order = 0;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Return))
		{
			SceneManager.LoadScene(2);
		}

		switch (order)
		{
		case 1:
			introTextActivate();
			break;
		case 2:
			introTwoTextActivate();
			break;
		case 3:
			introThreeTextActivate();
			break;
		case 4:
			meterArrowActivate();
			break;
		case 5:
			bulbArrowsActivate();
			break;
		case 6:
			canvasArrowActivate();
			break;
		case 7:
			astroArrowsActivate();
			break;
		case 8:
			spaceshipArrowsActivate();
			break;
		case 9:
			flagArrowActivate();
			break;
		case 10:
			pressSpaceToPlay();
			break;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			order++;
		}

		Debug.Log(order);
	}

	void introTextActivate()
	{
		text.text = intro2;
	}

	void bulbArrowsActivate()
	{
		meterArrow.SetActive(false);
		text.text = "How many baboons does it take to change a star? Who cares, just make sure to change them quick if they shatter!";
		bulbArrows.SetActive(true);
	}

	void meterArrowActivate()
	{
		text.text = intro3;
		meterArrow.SetActive(true);
		if (Input.GetKeyDown(KeyCode.Space))
		{
			bulbArrowsActivate();
		}
	}

	void introTwoTextActivate()
	{
		text.text = "In here, we provide the public with what they want to see. We make them believe. We're faking the moon! So buckle up kid.";
		if (Input.GetKeyDown(KeyCode.Space))
		{
			introThreeTextActivate();
		}
	}

	void introThreeTextActivate()
	{
		text.text = intro4;
	}

	void astroArrowsActivate()
	{
		canvasArrow.SetActive(false);
		text.text = "Under that bubble-looking helmet is Jim. Jim can't jump high, but on the moon you're supposed to be able to! Yank the 'zero gravity' rope to help him fly.";
		astroArrows.SetActive(true);
	}

	void spaceshipArrowsActivate()
	{
		astroArrows.SetActive(false);
		text.text = "And how will people be able to believe that Jim got to the moon in the first place? Well, not in an air baloon that's for sure. So if the spaceship looses air, pump it!";
		spaceshipArrows.SetActive(true);

	}

	void flagArrowActivate()
	{
		spaceshipArrows.SetActive(false);
		text.text = "And make sure that the star crossed banner stays straight. If it falls, raise it!";
		flagArrow.SetActive(true);            
	}

	void canvasArrowActivate()
	{
		bulbArrows.SetActive(false);
		text.text = "Also, we can't have space collapse on itself now, can we? If it falls off, stick it back up there!";
		canvasArrow.SetActive(true);
	}

	void pressSpaceToPlay()
	{
		flagArrow.SetActive(false);
		text.text = "We're going on air soon, so what are you waiting for? PRESS 'RETURN' AND GET READY!";
	}
}
