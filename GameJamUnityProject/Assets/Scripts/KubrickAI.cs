using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KubrickAI : MonoBehaviour {

	public ScoreBar scoreBar;
	public Image speech;
	public Text text;
	public ParticleSystem dust;

	public float kubrickStayTime;
	public float tutorialStayTime;
	private bool talking = false;
	private bool wintalking = false;
	private bool movingUp;
	private bool movingDown;

	private bool cooldown;
	private float cooldownTime;
	private float dialogueRoll;
	private Vector3 startposition;
	private Vector3 newPos;

	private string good = "You are doing great! The world believes in us!";
	private string good2 = "Ha ha! This is perfect! Next we're doing Mars!";

	private string medium = "Come on! Work harder! I will not let you embarass me like this!";
	private string medium2 = "Don't you get it! We WANT them to think we are ON THE MOON!";

	private string bad = "WHAT ARE YOU DOING YOU BLABBERING IDIOT!!!";
	private string bad2 = "AHHHHHHHHHHHHHHHHHH!! YOU ARE RUINING MY CAREER!";

	//TUTORIAL VOICE

	private bool intro1;
	private bool intro2bool;
	private bool intro3bool;
	private bool intro4bool;
	private bool lightbulbbool;
	private bool lightbulsuccesbool;
	private bool flagbool;
	private bool spaceshipbool;
	private bool zerogravitybool;
	private bool finishedbool;

	private string intro = "Alright, we're going live any minute now. Maurice couldn't be here today so you will have to. Have you ever been on a set before?";
	private string intro2 = "Agrh.. don't answer that. Of course you havn't, you're as young as my daughters. Let's make sure you know what to do.";
	private string intro3 = "You see that huge meter over there? Thanks to our friends at NASA that will tell us wether the world believes in us. Do I even have to say it? Keep it in the gren.";
	private string intro4 = "Lets begin.";
	private string lightbulb = "Well.. go fix the damn lightbulb. Take one from the crate and put it back in.";
	private string lightbulbsucces = "Atleast you know how to do that.";
	private string flag = "Do you think there is any gravity on the moon?? Raise the flag!";
	private string spaceship = "Last time I checked a spaceship doesn't do that. Pump some air in it!";
	private string zerogravity = "You cannot let that poor astronaut jump by himself. Raise him!";
	private string finished = "Alright.. that was not pretty but it will have to do. We are on any minute now! Dont fuck this up.";

	public bool winBool = false;
	public bool looseBool = false;

	public string win = "We did it! The world will forever remember this day! The day we portrayed men on the moon!!";
	public string loose = "NOOOOOOOOOOOOOO!!!! CUT CUT CUT!!!";

	public static KubrickAI instance; 

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);    
		}
	}

	void Start () 
	{
		newPos = new Vector3 (transform.position.x, transform.position.y + 5.5f, transform.position.z);
		startposition = transform.position;
	}

	void Update () 
	{
		if (winBool && !wintalking)
		{
			Debug.Log("You won!");
			wintalking = true;
			StartCoroutine(Reset(win, tutorialStayTime));
		}
		else if (looseBool && !wintalking)
		{
			Debug.Log("You lost!");
			wintalking = true;
			StartCoroutine(Reset(loose, kubrickStayTime));
		}

		if (intro1 && !talking)
		{
			talking = true;
			StartCoroutine(ShowKubrick(intro, tutorialStayTime));
		}
		else if (intro2bool)
		{
			talking = true;
			StartCoroutine(ShowKubrick(intro2, tutorialStayTime));
		}
		else if (intro3bool)
		{
			talking = true;
			StartCoroutine(ShowKubrick(intro3, tutorialStayTime));
		}
		else if (intro4bool)
		{
			talking = true;
			StartCoroutine(ShowKubrick(intro4, tutorialStayTime));
		}
		else if (lightbulbbool)
		{
			talking = true;
			StartCoroutine(ShowKubrick(lightbulb, tutorialStayTime));
		}
		else if (lightbulsuccesbool)
		{
			talking = true;
			StartCoroutine(ShowKubrick(lightbulbsucces, kubrickStayTime));
		}
		else if (flagbool)
		{
			talking = true;
			StartCoroutine(ShowKubrick(flag, tutorialStayTime));
		}
		else if (spaceshipbool)
		{
			talking = true;
			StartCoroutine(ShowKubrick(spaceship, tutorialStayTime));
		}
		else if (zerogravitybool)
		{
			talking = true;
			StartCoroutine(ShowKubrick(zerogravity, tutorialStayTime));
		}
		else if (finishedbool)
		{
			talking = true;
			StartCoroutine(ShowKubrick(finished, tutorialStayTime));
		}

		if (scoreBar.score >= 20f && scoreBar.score <= 30f && !talking && !cooldown)
		{
			cooldownTime = Random.Range(kubrickStayTime, kubrickStayTime + 3f);
			dialogueRoll = Random.Range(1, 3);
			Debug.Log("dialogueroll" + dialogueRoll);
			talking = true;
			cooldown = true;

			if (dialogueRoll == 1)
			{
				StartCoroutine(ShowKubrick(bad, kubrickStayTime));
			}
			else if (dialogueRoll == 2)
			{
				StartCoroutine(ShowKubrick(bad2, kubrickStayTime));
			}
			StartCoroutine(Cooldown(cooldownTime));
		}
		else if (scoreBar.score >= 50f && scoreBar.score <= 60f && !talking && !cooldown)
		{
			cooldownTime = Random.Range(kubrickStayTime, kubrickStayTime + 3f);
			dialogueRoll = Random.Range(1, 3);
			Debug.Log("dialogueroll" + dialogueRoll);
			talking = true;
			cooldown = true;

			if (dialogueRoll == 1)
			{
				StartCoroutine(ShowKubrick(medium, kubrickStayTime));
			}
			else if (dialogueRoll == 2)
			{
				StartCoroutine(ShowKubrick(medium2, kubrickStayTime));
			}
			StartCoroutine(Cooldown(cooldownTime));
		}
		else if (scoreBar.score >= 80f && scoreBar.score <= 90f && !talking && !cooldown)
		{
			cooldownTime = Random.Range(kubrickStayTime + 2f, kubrickStayTime + 4f);
			dialogueRoll = Random.Range(1, 3);
			Debug.Log("dialogueroll" + dialogueRoll);
			talking = true;
			cooldown = true;

			if (dialogueRoll == 1)
			{
				StartCoroutine(ShowKubrick(good, kubrickStayTime));
			}
			else if (dialogueRoll == 2)
			{
				StartCoroutine(ShowKubrick(good2, kubrickStayTime));
			}
			StartCoroutine(Cooldown(cooldownTime));
		}

		if(movingUp)
		{
			transform.position = Vector3.Lerp(transform.position, newPos, 0.1f);
		}
		if (movingDown)
		{
			transform.position = Vector3.Lerp(transform.position, startposition, 0.1f);
		}
	}

	public IEnumerator ShowKubrick(string voice, float stayTime)
	{

		//transform.position = new Vector3(transform.position.x, transform.position.y + 5.5f, transform.position.z);

		movingUp = true;
		movingDown = false;
		text.enabled = true;
		text.text = voice;
		speech.enabled = true;

		yield return new WaitForSeconds(stayTime);

		dust.Play();
		movingUp = false;
		movingDown = true;
		text.enabled = false;
		speech.enabled = false;
		talking = false;
		//transform.position = new Vector3(transform.position.x, transform.position.y - 5.5f, transform.position.z);
	}

	IEnumerator Cooldown(float time)
	{
		float elapsedTime = 0f;

		while(elapsedTime < time)
		{
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		cooldown = false;
	}

	public IEnumerator Reset(string voice, float stayTime)
	{
		yield return new WaitForEndOfFrame();

		Debug.Log("lost or won!");
		movingUp = true;
		text.text = voice;
		movingDown = false;
		text.enabled = true;
		speech.enabled = true;

		yield return new WaitForSeconds(stayTime);
	}
}
