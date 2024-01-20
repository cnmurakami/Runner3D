using UnityEngine;
using System.Collections;

/// <summary>
/// Attach this to destructibles objects and they will be destroyed with Projectile collision
/// </summary>
public class DestroyOnProjectile : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		if (col.CompareTag("Projectile")) {
			Destroy(this.gameObject);
		}
	}

	void OnTriggerStay(Collider col) {
		if (col.CompareTag("Projectile")) {
			Destroy(this.gameObject);
		}
	}
}
