﻿using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public int playerNum = -1;

	private static int MAX_NUM_PLAYERS = 4;

	private const string HORIZ_AXIS_STR = "Horizontal";
	private const string VERT_AXIS_STR = "Vertical";
	private const string CHARGE_BUTTON_STR = "Charge";
	private const string PLAYER_CONTROL_MODIFIER = "_p{0}"; // {0} replaced with player number 

	private Rigidbody rigidBody;

	public float normalMovementForce = 10.0f; // The torque to add when moving normally
	public float chargeMovementForce = 20.0f; // The torque to add when charging

	public float turnAssist = 10.0f; // Makes it easier to change direction
	public float assistThreshold = 1.0f; // How different must desired and actual direction be before turn assist is activate?

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		float horizAxis = Input.GetAxis(getControlInputName(HORIZ_AXIS_STR));
		float vertAxis = Input.GetAxis (getControlInputName(VERT_AXIS_STR));
		bool charging = Input.GetButton (getControlInputName(CHARGE_BUTTON_STR));

		Vector3 forceToApply = new Vector3 (
			horizAxis * (charging ? chargeMovementForce : normalMovementForce),
			0,
			vertAxis * (charging ? chargeMovementForce : normalMovementForce)
		);
		rigidBody.AddForce (forceToApply);


		// Turn assist
		if (turnAssist > 0) {
			Vector3 desiredDirection = new Vector3 (horizAxis, rigidBody.velocity.y, vertAxis);
			desiredDirection.Normalize ();

			Vector3 currentDirection = rigidBody.velocity;
			currentDirection.Normalize ();
		
			Vector3 assistDirection = desiredDirection - currentDirection;
			assistDirection.y = 0; // Do not assist in the up/down direction.

			if(assistDirection.magnitude > assistThreshold) {
				rigidBody.AddForce (assistDirection * turnAssist);
			}
		}
	}

	/// <summary>
	/// Creates the name of the input control to check
	/// Given the base control and the player number
	/// </summary>
	/// <returns>The control input string.</returns>
	/// <param name="baseStr">The desired input type (Horizontal, Vertical, Charge, etc).</param>
	private string getControlInputName(string baseStr) {
		if (playerNum < 0 || playerNum > MAX_NUM_PLAYERS) {
			Debug.LogError(string.Format("Player number {0} is not valid. " +
				"Please update the player number in the player movement script.", playerNum));
		}
		return string.Format (baseStr + PLAYER_CONTROL_MODIFIER, playerNum);
	}

}