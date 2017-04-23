using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

    public Image speech;
    public Text text;

    public GameObject bulbArrows, meterArrow, astroArrows, spaceshipArrows, flagArrow, canvasArrow;

    public float readTimer;

    private string intro = "Alright, we're going live any minute now. Maurice couldn't be here today so you will have to. Have you ever been on a set before?";
    private string intro2 = "Agrh.. don't answer that. Of course you havn't, you're as young as my daughters. Let's make sure you know what to do.";
    private string intro3 = "You see that huge meter over there? Thanks to our friends at NASA, that will tell us wether the world believes in us. Do I even have to say it? Keep it in the green.";
    private string intro4 = "We have to make them believe that we were on the moon, okay? Lets begin.";
    private string lightbulb = "Well.. go fix the damn lightbulb. Take one from the crate and put it back in.";
    private string lightbulbsucces = "Atleast you know how to do that.";
    private string flag = "Do you think there is any gravity on the moon?? Raise the flag!";
    private string spaceship = "Last time I checked a spaceship doesn't do that. Pump some air in it!";
    private string zerogravity = "You cannot let that poor astronaut jump by himself. Raise him!";
    private string finished = "Alright.. that was not pretty but it will have to do. We are on any minute now! Dont fuck this up.";

    // Use this for initialization
    void Start () {
        text.text = intro;
        Invoke("introTextActivate", readTimer * 1.3f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(2);
        }
	}

    void introTextActivate()
    {
        text.text = intro2;
        Invoke("introTwoTextActivate", readTimer);
    }

    void bulbArrowsActivate()
    {
        meterArrow.SetActive(false);
        text.text = "How many baboons does it take to change a star? Who cares, just make sure to change them quick if they shatter!";
        bulbArrows.SetActive(true);
        Invoke("canvasArrowActivate", readTimer * 1.7f);
    }

    void meterArrowActivate()
    {
        text.text = intro3;
        meterArrow.SetActive(true);
        Invoke("bulbArrowsActivate", readTimer * 1.3f);
    }

    void introTwoTextActivate()
    {
        text.text = "In here, we provide the public with what they want to see. We make them believe. We're faking the moon! So buckle up kid.";
        Invoke("introThreeTextActivate", readTimer);
    }

    void introThreeTextActivate()
    {
        text.text = intro4;
        Invoke("meterArrowActivate", readTimer / 2.0f);
    }

    void astroArrowsActivate()
    {
        canvasArrow.SetActive(false);
        text.text = "Under that bubble-looking helmet is Jim. Jim can't jump high, but on the moon you're supposed to be able to! Yank the 'zero gravity' rope to help him fly.";
        astroArrows.SetActive(true);
        Invoke("spaceshipArrowsActivate", readTimer * 1.3f);
    }

    void spaceshipArrowsActivate()
    {
        astroArrows.SetActive(false);
        text.text = "And how will people be able to believe that Jim got to the moon in the first place? Well, not in an air baloon that's for sure. So if the spaceship looses air, pump it!";
        spaceshipArrows.SetActive(true);
        Invoke("flagArrowActivate", readTimer * 1.4f);
    }

    void flagArrowActivate()
    {
        spaceshipArrows.SetActive(false);
        text.text = "And make sure that the star crossed banner stays straight. If it falls, raise it!";
        flagArrow.SetActive(true);
        Invoke("pressSpaceToPlay", readTimer);
    }

    void canvasArrowActivate()
    {
        bulbArrows.SetActive(false);
        text.text = "Also, we can't have space collapse on itself now, can we? If it falls off, stick it back up there!";
        canvasArrow.SetActive(true);
        Invoke("astroArrowsActivate", readTimer);
    }

    void pressSpaceToPlay()
    {
        flagArrow.SetActive(false);
        text.text = "We're going on air soon, so what are you waiting for? PRESS SPACE AND GET READY!";
    }
}
