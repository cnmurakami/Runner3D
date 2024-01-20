using UnityEngine;
using System.Collections;

/// <summary>
/// Checks if player can jump (not already in the air)
/// </summary>
public class PlayerCheckJump : MonoBehaviour {
	public static bool isGrounded;

	void Awake() {
		isGrounded=true;
	}

	/// <summary>
	/// Checks wheter the player is colliding with an object to enable jumping
	/// </summary>
	void OnCollisionStay(Collision collisionInfo) {
		isGrounded=true;
	}

	/// <summary>
	/// Checks wheter the player is colliding with an object to disable jumping
	/// </summary>
	void OnCollisionExit(Collision collisionInfo) {
		isGrounded=false;
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.CompareTag("DisableDuck")) {
			GetComponentInParent<PlayerController>().isIdle=false;
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.CompareTag("DisableDuck")) {
			GetComponentInParent<PlayerController>().isIdle=true;
		}
	}
}
