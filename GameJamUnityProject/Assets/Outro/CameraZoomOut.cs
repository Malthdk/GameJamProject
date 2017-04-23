using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomOut : MonoBehaviour {

    Camera cam;

    [SerializeField]
    Transform startPos;
    [SerializeField]
    Transform endPos;

    [SerializeField]
    float startZoom;
    [SerializeField]
    float endZoom;

    [SerializeField]
    float zoomSpeed;

    float i;

	void Awake () {
        cam = GetComponent<Camera>();

        i = 0.0f;
	}

    void FixedUpdate()
    {
        CameraZoom();
    }

    void CameraZoom()
    {
        float rate = 1.0f / zoomSpeed;

        if (i < 1.0f)
        {
            i += Time.deltaTime * rate;

            cam.orthographicSize = Mathf.Lerp(startZoom, endZoom, Mathf.SmoothStep(0.0f,1.0f,i));
            transform.position = Vector3.Lerp(startPos.position, endPos.position, Mathf.SmoothStep(0.0f, 1.0f, i));
        }
    }
}
