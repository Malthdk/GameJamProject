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

	private bool talking = false;
	private bool movingUp;
	private bool movingDown;

	private bool cooldown;
	private float cooldownTime;
	private Vector3 startposition;
	private Vector3 newPos;

	private string good = "You are doing great! The world believes in us!";
	private string medium = "Come on! Work harder! I will not let you embaress me like this!";
	private string bad = "WTF ARE YOU DOING YOU BLABBERING IDIOT!!!";

	void Start () 
	{
		newPos = new Vector3 (transform.position.x, transform.position.y + 12f, transform.position.z);
		startposition = transform.position;
	}

	void Update () 
	{
		if (scoreBar.score >= 20f && scoreBar.score <= 30f && !talking && !cooldown)
		{
			cooldownTime = Random.Range(kubrickStayTime, kubrickStayTime + 3f);
			talking = true;
			cooldown = true;

			StartCoroutine(ShowKubrick(bad));
			StartCoroutine(Cooldown(cooldownTime));
		}
		else if (scoreBar.score >= 50f && scoreBar.score <= 60f && !talking && !cooldown)
		{
			cooldownTime = Random.Range(kubrickStayTime, kubrickStayTime + 3f);
			talking = true;
			cooldown = true;

			StartCoroutine(ShowKubrick(medium));
			StartCoroutine(Cooldown(cooldownTime));
		}
		else if (scoreBar.score >= 80f && scoreBar.score <= 90f && !talking && !cooldown)
		{
			cooldownTime = Random.Range(kubrickStayTime + 2f, kubrickStayTime + 4f);
			talking = true;
			cooldown = true;

			StartCoroutine(ShowKubrick(good));
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

	public IEnumerator ShowKubrick(string voice)
	{

		//transform.position = new Vector3(transform.position.x, transform.position.y + 5.5f, transform.position.z);

		movingUp = true;
		movingDown = true;
		text.enabled = true;
		text.text = voice;
		speech.enabled = true;

		yield return new WaitForSeconds(kubrickStayTime);

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
}
