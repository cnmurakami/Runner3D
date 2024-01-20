using UnityEngine;
using System.Collections;

/// <summary>
/// Makes some of the target practice obstacles to suddenly turn when the player gets near
/// </summary>
public class SurpriseUp : MonoBehaviour {
	protected bool hasAppeared=false;
	public GameObject player;

	/// <summary>
	///	Because the obstacle has to check if it is at the same position of a ramp or hole, it is impossible to disable its collider at the start
	/// and it is needed to disable the collider at the start because otherwise the player could randomly shoot it to see if it disappears
	/// </summary>
	void Start() {
		this.transform.Rotate(0, 90, 0);
		StartCoroutine(DisableCollider());
	}

	/// <summary>
	/// Waits for the player to get closer to face him and reactivates the collider
	/// </summary>
	void FixedUpdate() {
		if (this.transform.position.z-Camera.main.transform.position.z<=3.5f && hasAppeared==false) {
			StartCoroutine(Appear());
			this.GetComponent<Collider>().enabled=true;
			hasAppeared=true;
		}
	}

	/// <summary>
	/// Disables collider after 1 second counting from the time the game starts
	/// </summary>
	IEnumerator DisableCollider() {
		yield return new WaitForSeconds(1);
		this.GetComponent<Collider>().enabled=false;
	}

	/// <summary>
	/// Turns 90 degrees to face the player
	/// </summary>
	IEnumerator Appear() {
		int rotation=0;
		while (rotation<=90) {
			this.transform.Rotate(0, -10, 0);
			yield return new WaitForSeconds(0.01f);
			rotation+=10;
		}
	}
}
