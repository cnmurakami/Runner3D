using UnityEngine;
using System.Collections;

/// <summary>
/// Creates a running effect on the camera
/// </summary>
public class CameraRunningEffect : MonoBehaviour {
	[Tooltip("Player GameObject")]
	public GameObject Player;
	protected Vector3 startPos;
	protected Vector3 leftPos;
	protected Vector3 rightPos;
	protected int directionRL;
	protected int directionUD;
	protected float step;
	protected float wait;
	protected bool isStarted;

	/// <summary>
	/// Setup of speeds and positions
	/// </summary>
	void Start() {
		isStarted=true;
		step=0.0125f;
		wait=0.00001f;
		directionUD=-1;
		directionRL=1;
		startPos=this.transform.localPosition;
		rightPos=new Vector3(startPos.x+0.1f, startPos.y-0.2f, startPos.z);
		leftPos=new Vector3(startPos.x-0.1f, startPos.y-0.2f, startPos.z);
		StartCoroutine(RunningEffect());
	}

	/// <summary>
	/// As long as the player is not doing any action besides running, effect will be active
	/// </summary>
	void FixedUpdate() {
		if (Player.GetComponent<PlayerController>().isIdle==true && PlayerCheckJump.isGrounded==true && isStarted==false && ScreenManager.isPaused==false) {
			StartCoroutine(RunningEffect());
			isStarted=true;
		}
	}

	/// <summary>
	/// Takes the default position points and moves the camera along them
	/// </summary>
	/// <returns>The effect.</returns>
	IEnumerator RunningEffect() {
		while (Player.GetComponent<PlayerController>().isIdle==true && PlayerCheckJump.isGrounded==true && ScreenManager.isPaused==false) {
			if (directionUD==-1 && directionRL==1) {
				this.transform.localPosition=new Vector3(transform.localPosition.x+step, transform.localPosition.y-step*2, transform.localPosition.z);
				yield return new WaitForSeconds(wait);
				if (this.transform.localPosition.x>=rightPos.x) {
					this.transform.localPosition=new Vector3(rightPos.x, rightPos.y, transform.localPosition.z);
					directionUD=1;
					directionRL=-1;
				}
			}
			if (directionUD==1 && directionRL==-1) {
				this.transform.localPosition=new Vector3(transform.localPosition.x-step, transform.localPosition.y+step*2, transform.localPosition.z);
				yield return new WaitForSeconds(wait);
				if (this.transform.localPosition.x<=startPos.x) {
					this.transform.localPosition=new Vector3(startPos.x, startPos.y, transform.localPosition.z);
					directionUD=-1;
					directionRL=-1;
				}
			}
			if (directionUD==-1 && directionRL==-1) {
				this.transform.localPosition=new Vector3(transform.localPosition.x-step, transform.localPosition.y-step*2, transform.localPosition.z);
				yield return new WaitForSeconds(wait);
				if (this.transform.localPosition.x<=leftPos.x) {
					this.transform.localPosition=new Vector3(leftPos.x, leftPos.y, transform.localPosition.z);
					directionUD=1;
					directionRL=1;
				}
			}
			if (directionUD==1 && directionRL==1) {
				this.transform.localPosition=new Vector3(transform.localPosition.x+step, transform.localPosition.y+step*2, transform.localPosition.z);
				yield return new WaitForSeconds(wait);
				if (this.transform.localPosition.x>=startPos.x) {
					this.transform.localPosition=new Vector3(startPos.x, startPos.y, transform.localPosition.z);
					directionUD=-1;
					directionRL=1;
				}
			}
		}
		this.transform.localPosition=new Vector3(startPos.x, startPos.y, transform.localPosition.z);
		isStarted=false;
	}
}
