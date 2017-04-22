using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour {

	public static ScoreBar instance;

	public static float scoreDecreaseMultiplier = 1f;
	public float scoreIncreaseMultiplier = 1.5f;

	float fillAmount;
	public Image content;
	public float decreaseAmount;
	public float decreaseTime;

	[RangeAttribute(0, 100)]
	public float score;

	private float minScore = 0f;
	private float maxScore = 100f;
	private float minYValue = 0f;
	private float maxYvalue = 1f;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);    
		}
	}

	void Start()
	{
		StartCoroutine(IncreaseScore());
	}

	void Update()
	{
		HandleBar();
		IncreaseScore();
	}

	public IEnumerator IncreaseScore()
	{
		float elapsedTime = 0;
		while (score <= maxScore)
		{
			if (scoreDecreaseMultiplier < 1f) {
				scoreDecreaseMultiplier = 1f;
			}
		
			score += decreaseAmount * scoreIncreaseMultiplier;
			score -= decreaseAmount * scoreDecreaseMultiplier;

			if (score > 100f)
			{
				score = 100f;
			}
			yield return new WaitForSeconds(decreaseTime);

		}
	}

	void HandleBar()
	{
		content.fillAmount = Map(score, minScore, maxScore, minYValue, maxYvalue);

		if (score > maxScore/2)
		{
			content.color = new Color32((byte)Map(score, maxScore/2, maxScore, 255, 0), 255, 0, 255);
		}
		else
		{
			content.color = new Color32(255,(byte)Map(score,0,maxScore/2, 0, 255), 0, 255);
		}
	}

	public float Map(float value, float inMin, float inMax, float outMin, float outMax)
	{
		return(value-inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}