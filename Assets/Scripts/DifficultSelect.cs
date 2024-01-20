using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Changes in game difficult
/// </summary>
public class DifficultSelect : MonoBehaviour {
	public Slider difficultChanger;
	public Text difficultText;
	public static int difficult;

	void Start() {
		Cursor.visible=true;
		difficult=(int)difficultChanger.value;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
			difficultChanger.value+=1;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
			difficultChanger.value-=1;
		}
		difficult=(int)difficultChanger.value;
		switch (difficult) {
			case 0:
				difficultText.text="Easy";
				break;
			case 1:
				difficultText.text="Normal";
				break;
			case 2:
				difficultText.text="Hard";
				break;
			case 3:
				difficultText.text="Insane";
				break;
		}
	}
}
