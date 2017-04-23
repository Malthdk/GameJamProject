using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardHandler : MonoBehaviour {

    [SerializeField]
    GameObject outsideScreenPos;

    [SerializeField]
    GameObject insideScreenPos;

    [SerializeField]
    float flySpeed;

    [SerializeField]
    float waitTimer;

    float rate, i, j;

    Animator clipboardAnimator;

    bool hasFlownUp;

	// Use this for initialization
	void Start () {
        rate = 1.0f / flySpeed;
        i = 0.0f;
        j = 0.0f;

        clipboardAnimator = GetComponent<Animator>();

        hasFlownUp = false;
    }
	
	// Update is called once per frame
	void Update () {
            FlyUp();
    }

    void FlyUp()
    {
        if (i < 1.0f)
        {
            i += Time.deltaTime * rate;

            transform.position = Vector2.Lerp(outsideScreenPos.transform.position, insideScreenPos.transform.position, Mathf.SmoothStep(0.0f, 1.0f, i));
        }

        if (i > 1.0f)
        {
            hasFlownUp = true;
            Invoke("ActivateClapAnim", waitTimer);
        }
    }

    void ActivateClapAnim()
    {
        clipboardAnimator.SetBool("clipboardClapActivate", true);
    }

    void FlyDown()
    {
        if (j < 1.0f)
        {
            j += Time.deltaTime * rate;

            transform.position = Vector2.Lerp(insideScreenPos.transform.position, outsideScreenPos.transform.position, Mathf.SmoothStep(0.0f, 1.0f, j));
        }

        if (j > 0.9f)
        {
            Destroy(insideScreenPos);
            Destroy(outsideScreenPos);
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
