using UnityEngine;
using System.Collections;

/// <summary>
/// Checks Player collisions and changes game states
/// </summary>
public class PlayerCheckCollision : MonoBehaviour {
	/// <summary>
	/// Stores game states
	/// 0=running
	/// 1=win
	/// -1=lose
	/// </summary>
	public static int condition=0;

	void Awake() {
		condition=0;
	}

	/// <summary>
	/// Checks if Player collides with an obstacle and set condition
	/// </summary>
	void OnTriggerEnter(Collider collider) {
		if (collider.CompareTag("Obstacle")) {
			condition=-1;
		}
		else if (collider.CompareTag("Finish")) {
			condition=1;
		}
	}
}
