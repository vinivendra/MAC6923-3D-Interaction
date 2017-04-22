using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour {

	private float translationSpeed;
	public float signal;

	void Start ()
	{
		float baseSpeed = 15;
		float variation = (60 * Random.value);
		float normalizationFactor = 50 / transform.position.z;
		translationSpeed = signal * (baseSpeed + variation) * normalizationFactor; // degrees per second
	}

	// Update is called once per frame
	void Update () {
		float ratio = Time.deltaTime;
		float rotationSpeed = 180; // degrees per second
	
		transform.Rotate (new Vector3 (rotationSpeed * ratio, 0, 0));

		Vector3 origin = new Vector3 (0, 0, 0);
		Vector3 yAxis = new Vector3 (0, 1, 0);

		transform.RotateAround (origin, yAxis, translationSpeed * ratio);
	}
}
