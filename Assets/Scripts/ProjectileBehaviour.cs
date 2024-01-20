using UnityEngine;
using System.Collections;

/// <summary>
/// Moves the projectile ahead and controls its lifespan
/// </summary>
public class ProjectileBehaviour : MonoBehaviour {
	protected float start;
	protected float distance;

	/// <summary>
	/// Set the starting position of the projectile at its spawn position
	/// </summary>
	void Start() {
		start=this.transform.position.z;
		distance=GameObject.Find("CreateLevel").GetComponent<CreateLevel>().distance;
	}

	/// <summary>
	/// Moves the projectile at double Player's speed
	/// Projectile is destroyed if it hits a object or if it runs 3 times the distance between obstacles
	/// </summary>
	void FixedUpdate() {
		if (ScreenManager.isPaused==false) {
			this.transform.position+=new Vector3(0, 0, GameObject.Find("Player").GetComponent<PlayerController>().speed*3*Time.timeScale);
		}
		if (this.transform.position.z-start>=distance*2*PlayerController.block) {
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.CompareTag("Obstacle")) {
			Destroy(this.gameObject);
		}
	}
}
