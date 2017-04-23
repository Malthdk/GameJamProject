using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroFade : MonoBehaviour {

    // Scriptet styrer OGSÅ hvornår Clipboard bliver active
    [SerializeField]
    GameObject clipboard;

    Color currCol;

    [SerializeField]
    float fadeInSpeed;

    float i;

    // Use this for initialization
    void Start()
    {
        currCol = Color.black;

        i = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        FadeIn();

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

        if (i >= 0.7f)
        {
            clipboard.SetActive(true);
        }

        if (i >= 1.0f)
        {
			Invoke ("ChangeScene", 5f);
        }
    }
		
	void ChangeScene(){
		SceneManager.LoadScene (1);
	}

}
