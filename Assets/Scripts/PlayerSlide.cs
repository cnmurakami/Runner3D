using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Make the player slide to pass under objects
/// </summary>
public class PlayerSlide : MonoBehaviour {

	/// <summary>
	/// Get Keyboard Inputs and execute Player's actions
	/// </summary>
	void Update() {
		if (ScreenManager.isPaused==false) {
			/// 'Sliding'
			if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && PlayerCheckJump.isGrounded==true && GetComponentInParent<PlayerController>().isIdle==true) {
				StartCoroutine(Sliding());
			}
		}
	}

	/// <summary>
	/// Makes the player slide for a short time then get back up by changing its scale
	/// </summary>
	IEnumerator Sliding() {
		GetComponentInParent<PlayerController>().isIdle=false;
		/// Because the Jump action is using rigidbody forces, it is need to be disable to avoid unwanted reactions
		GetComponentInParent<PlayerController>().jumper.GetComponent<Rigidbody>().detectCollisions=false;
		GetComponentInParent<PlayerController>().jumper.GetComponent<Rigidbody>().useGravity=false;
		while (this.transform.localScale.y>0.3f) {
			this.transform.localScale=new Vector3(1, transform.localScale.y-0.15f, 1);
			yield return new WaitForSeconds(0.0001f);
		}

		this.transform.localScale=new Vector3(1, 0.3f, 1);

		/// Time to be waited before getting up
		yield return new WaitForSeconds(0.4f);

		while (this.transform.localScale.y<1) {
			this.transform.localScale=new Vector3(1, transform.localScale.y+0.1f, 1);
			yield return new WaitForSeconds(0.0001f);
		}

		this.transform.localScale=new Vector3(1, 1, 1);
		GetComponentInParent<PlayerController>().isIdle=true;
		GetComponentInParent<PlayerController>().jumper.GetComponent<Rigidbody>().detectCollisions=true;
		GetComponentInParent<PlayerController>().jumper.GetComponent<Rigidbody>().useGravity=true;
		yield break;
	}

}
