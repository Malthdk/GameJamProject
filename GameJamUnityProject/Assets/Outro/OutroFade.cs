using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroFade : MonoBehaviour {

    Color currCol;

    [SerializeField]
    float fadeInSpeed;
    [SerializeField]
    float fadeOutSpeed;

    [SerializeField]
    float fadeOutTimer;

    float i;
    bool hasFadedIn;

	// Use this for initialization
	void Start () {
        currCol = Color.black;

        i = 0.0f;
        hasFadedIn = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasFadedIn)
        FadeIn();

        Invoke("FadeOut", fadeOutTimer);

        GetComponent<SpriteRenderer>().color = currCol;
	}


    void FadeIn()
    {
        float rate = 1.0f / fadeInSpeed;

        if (i < 1.0f)
        {
            i += Time.deltaTime * rate;

            currCol = Color.Lerp(Color.black, Color.clear, i);
        }

        if (i >= 1.0f)
        {
            i = 0.0f;
            hasFadedIn = true;
        }
    }

    void FadeOut()
    {
        float rate = 1.0f / fadeOutSpeed;

        if (i < 1.0f)
        {
            i += Time.deltaTime * rate;

            currCol = Color.Lerp(Color.clear, Color.black, i);
			SceneManager.LoadScene (0);
        }
    }
}
