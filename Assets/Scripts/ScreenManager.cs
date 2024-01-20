using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages general screens while stage is running
/// </summary>
public class ScreenManager : MonoBehaviour {
	/// Public declaration of the screens
	public Canvas pause;
	public Canvas win;
	public Canvas gameOver;
	public Canvas start;
	public Text failedText;
	public Text succeededText;
	public Text startText;
	public Text countdown;

	//Public static variable passing Pause status
	public static bool isPaused;
	//Stores current timeScale
	public float currentTime;
	//Notifies game can start
	public bool canStart=false;
	private int subject;

	/// <summary>
	/// Disables unwanted screens at start and pauses to start countdown
	/// </summary>
	void Awake() {
		isPaused=true;
		pause.enabled=false;
		win.enabled=false;
		gameOver.enabled=false;
	}

	/// <summary>
	/// Starts Countdown to begin the game
	/// </summary>
	void Start() {
		subject=Random.Range(1, 10000);
		StartCoroutine(Countdown());
	}

	/// <summary>
	/// Checks whevener the player press pause, die or win the game
	/// </summary>
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.Space) && isPaused==true)) {
			Pause();
		}
		if (PlayerCheckCollision.condition==1) {
			Time.timeScale=0;
			succeededText.text="Subject #"+subject.ToString()+" completed the test. Congratulations.\nReady for the next one?";
			isPaused=true;
			win.enabled=true;
			EndGame();
		}
		if (PlayerCheckCollision.condition==-1) {
			Time.timeScale=0;
			failedText.text="Subject #"+subject.ToString()+" has died.\nLoad a more suited subject?";
			isPaused=true;
			gameOver.enabled=true;
			EndGame();
		}
	}

	/// <summary>
	/// Ends the current game and waits player action
	/// </summary>
	void EndGame() {
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
			PlayerCheckCollision.condition=0;
			SceneManager.LoadScene("Stage");
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			PlayerCheckCollision.condition=0;
			SceneManager.LoadScene("Titlescreen");
		}
	}

	/// <summary>
	/// Countdown to start game
	/// </summary>
	IEnumerator Countdown() {
		startText.text="Subject #"+subject.ToString()+" test is starting in...";
		countdown.text="3";
		yield return new WaitForSeconds(1);
		countdown.text="2";
		yield return new WaitForSeconds(1);
		countdown.text="1";
		yield return new WaitForSeconds(1);
		countdown.text="NOW!";
		yield return new WaitForSeconds(1);
		isPaused=false;
		start.enabled=false;
		canStart=true;
		yield break;
	}

	/// <summary>
	/// Manages paused state
	/// </summary>
	void Pause() {
		if (canStart==true) {
			// Whenever pause is activated, it stores current timeScale to currentTime and changes it to 0
			if (isPaused==false) {
				currentTime=Time.timeScale;
				Time.timeScale=0;
				pause.enabled=true;
				isPaused=true;
			}
			// When deactivated, pause restores timeScale from currentTime
			else if (PlayerCheckCollision.condition==0) {
				Time.timeScale=currentTime;
				pause.enabled=false;
				isPaused=false;
			}
		}
	}

	/// <summary>
	/// Go back to Title Screen
	/// </summary>
	public void Terminate() {
		SceneManager.LoadScene("Titlescreen");
	}
}
