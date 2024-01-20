using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Controls player basic movements
/// </summary>
public class PlayerController : MonoBehaviour {
	[Tooltip("Base player movement speed")]
	public float speed=0.05f;
	[Tooltip("Amount of time to be increased to timeScale on Fixed Update")]
	public float step=0.0005f;
	public GameObject jumper;

	//Distance between blocks to be moved to
	public static float block=0.5f;
	//Jump force
	protected float jumpSpeed;
	//Checks if the player is doing any action
	public bool isIdle=true;
	//Stores player current block position (ranges from 0 to 2)
	protected int currentPos=1;
	//List of possitions based on blocks
	protected float[] pos;
	//Checks if the player can move
	public bool canMove=true;


	/// <summary>
	/// Starts Player's Jump Speed and stores possible positions
	/// </summary>
	void Awake() {
		Application.targetFrameRate=60;
		pos=new float[3];
		pos[0]=-block;
		pos[1]=0;
		pos[2]=block;
		Time.timeScale=1f;
		jumpSpeed=150f/Time.timeScale;
	}

	/// <summary>
	/// Moves the player along Z axis while increasing its speed by increasing timeScale
	/// </summary>
	void FixedUpdate() {
		if (ScreenManager.isPaused==false) {
			this.transform.position=new Vector3(transform.position.x, transform.position.y, transform.position.z+speed);
			Time.timeScale+=step;
		}
	}

	/// <summary>
	/// Get Keyboard Inputs and execute Player's actions
	/// </summary>
	void Update() {
		if (ScreenManager.isPaused==false) {
			/// Move to left
			if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && currentPos>0 && canMove==true) {
				currentPos--;
				StartCoroutine(Move(pos[currentPos]));
			}
			/// Move to right
			if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && currentPos<2 && canMove==true) {
				currentPos++;
				StartCoroutine(Move(pos[currentPos]));
			}
			/// Jump
			if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && PlayerCheckJump.isGrounded==true && isIdle==true) {
				jumper.GetComponent<Rigidbody>().AddForce(Vector3.up*jumpSpeed);
			}
		}
	}

	/// <summary>
	/// Moves the player along X axis to designated space
	/// </summary>
	/// <param name="block">Block to be moved to</param>
	IEnumerator Move(float block) {
		/// Checks if the player is moving to the right
		if (block-this.transform.position.x>0) {
			while (this.transform.position.x<block) {
				/// If the Player decides to move to opposite direction before the movement ends, break the cycle
				if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
					break;
				}
				this.transform.position=new Vector3(transform.position.x+0.05f, transform.position.y, transform.position.z);
				yield return new WaitForSeconds(0.001f);
			}
		}
		/// If not moving to the right, move to the left
		else {
			while (this.transform.position.x>block) {
				/// If the Player decides to move to opposite direction before the movement ends, break the cycle
				if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
					break;
				}
				this.transform.position=new Vector3(transform.position.x-0.05f, transform.position.y, transform.position.z);
				yield return new WaitForSeconds(0.001f);
			}
		}
		/// Even if movement is not completed, ensures the player will be at the right block position
		this.transform.position=new Vector3(block, transform.position.y, transform.position.z);
		yield break;
	}

	/// <summary>
	/// While the player is moving alongside a ramp, disables its movements
	/// </summary>
	void OnTriggerEnter(Collider col) {
		if (col.CompareTag("Ramp")) {
			canMove=false;
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.CompareTag("Ramp")) {
			canMove=true;
		}
	}
}
