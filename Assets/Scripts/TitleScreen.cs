using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the title screen canvas and inputs
/// </summary>
public class TitleScreen : MonoBehaviour {
	[Tooltip("Canvas of the title screen")]
	public Canvas title;
	[Tooltip("Canvas of the instructions screen")]
	public Canvas instructions;

	/// <summary>
	/// Disable unwanted canvas at start
	/// </summary>
	void Start() {
		title.enabled=true;
		instructions.enabled=false;
	}

	/// <summary>
	/// Gets inputs from the player and executes actions
	/// </summary>
	void Update() {
		if (Input.GetKeyDown(KeyCode.Return)) {
			title.enabled=false;
			instructions.enabled=true;
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			SceneManager.LoadScene("Stage");
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}
}
