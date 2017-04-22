// TODO: Add bounce to truck/fence collision (reflect velocity?)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float maxSpeed;
	public Text scoreText;
	public Text gameOverText;

	private Rigidbody rigidBody;

	private bool isStillPlaying = true;
	private int score = 0;

	void Start ()
	{
		rigidBody = GetComponent<Rigidbody> ();
		UpdateScoreText ();
		gameOverText.text = "";
	}

	void OnCollisionEnter (Collision collision)
	{
		GameObject other = collision.collider.gameObject;
		if (other.CompareTag ("House")) {
			isStillPlaying = false;
			ThrowPlayerAwayFrom (other.transform.position);
			gameOverText.text = "You lose!";
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);

			score++;
			UpdateScoreText ();

			if (score >= 12) {
				gameOverText.text = "You win!";
			}
		}
	}

	void FixedUpdate ()
	{
		if (!isStillPlaying) {
			return;
		}

		float steeringAcceleration = 600;
		float acceleration = 60;
		float maxSpeedSqr = maxSpeed * maxSpeed;
		float maxAngularSpeedSqr = 1.5f * 1.5f;
		float sidewaysFriction = 2;

		float ratio = Time.deltaTime * 50;

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// Steer
		if (rigidBody.angularVelocity.sqrMagnitude <= maxAngularSpeedSqr) {
			Vector3 steering = new Vector3 (0, moveHorizontal, 0);
			rigidBody.AddRelativeTorque (steering * steeringAcceleration * ratio);
		}

		// Accelerate/Break
		if (rigidBody.velocity.sqrMagnitude <= maxSpeedSqr) {
			Vector3 movement = new Vector3 (0, 0, moveVertical);
			rigidBody.AddRelativeForce (movement * acceleration * ratio);
		}

		// Friction
		bool truckIsAccelerating = Mathf.Abs (moveVertical) > 0.001;

		if (truckIsAccelerating) {
			ApplySlidingFriction (ratio, sidewaysFriction);
		} else {
			ApplyFriction (ratio);
		}

		bool truckIsSteering = Mathf.Abs (moveHorizontal) > 0.001;
		if (!truckIsSteering) {
			ApplySteeringFriction (ratio);
		}

		// Stop the truck from flipping
		Vector3 angles = rigidBody.transform.rotation.eulerAngles;
		rigidBody.transform.Rotate(new Vector3 (-angles.x, 0, -angles.z));
	}


	//
	void ApplySlidingFriction (float ratio, float sidewaysFriction) {
		Vector3 orientation = transform.rotation * new Vector3 (0, 0, 1);
		float projectionMagnitude = Vector3.Dot (orientation, rigidBody.velocity);
		Vector3 projection = projectionMagnitude * orientation;
		Vector3 slide = rigidBody.velocity - projection;
		Vector3 slideCorrection = slide * (-1);
		rigidBody.AddForce (slideCorrection * sidewaysFriction * ratio);
	}

	void ApplyFriction (float ratio) {
		rigidBody.velocity *= 0.9f * ratio;
	}

	void ApplySteeringFriction (float ratio) {
		rigidBody.angularVelocity *= 0.7f * ratio;
	}

	//
	void ThrowPlayerAwayFrom (Vector3 point) {
		float throwingIntensity = 5;

		// translation
		Vector3 awayFromHouse = point - this.transform.position;
		Vector3 throwDirection = awayFromHouse + new Vector3 (0, 1, 0);
		rigidBody.velocity = throwDirection * throwingIntensity;
	}

	//
	void UpdateScoreText()
	{
		scoreText.text = "Score: " + score;
	}
		
}
