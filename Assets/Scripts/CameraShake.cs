using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	
	// How long the object should shake for.
	public float shakeDuration = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount;
	public float decreaseFactor = 2.0f;
	
	Vector3 originalPos;
	
	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}
	
	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
        int clicks = GameObject.Find("GameManager").GetComponent<Game>().clicks;
        bool state = GameObject.Find("GameManager").GetComponent<Game>().GameOver;

        if (clicks > 50 && !state) shakeAmount = ((float)clicks - 50) / 5000;
        if(state && shakeAmount > 0) shakeAmount -= Time.deltaTime / 50;
		if (shakeAmount > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
			//shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			camTransform.localPosition = originalPos;
		}
	}
}
