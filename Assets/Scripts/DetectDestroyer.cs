using UnityEngine;
using System.Collections;

/// <summary>
/// Destroy objects if they are overlapping the ramp or hole. Can be customized to not detected ramp
/// </summary>
public class DetectDestroyer : MonoBehaviour {
	public bool destroyOnRamp=true;

	void OnTriggerStay(Collider collider) {
		if ((destroyOnRamp==true && collider.CompareTag("Ramp")) || collider.CompareTag("Destroyer")) {
			Destroy(this.gameObject);
		}
	}
}
