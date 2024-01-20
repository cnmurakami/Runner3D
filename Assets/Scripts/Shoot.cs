using UnityEngine;
using System.Collections;

/// <summary>
/// Manages player shooting, reload time and generates its projectiles
/// </summary>
public class Shoot : MonoBehaviour {
	public GameObject bullet;
	public GameObject jumper;
	protected float reload;

	/// <summary>
	/// Stores initial time of reload
	/// </summary>
	void Start() {
		reload=Time.time;
	}

	/// <summary>
	/// Whenever player presses Space key and at least 1 second has passed since last reload, it spawns a projectile and stores new reload time
	/// </summary>
	void Update() {
		if (ScreenManager.isPaused==false) {
			if (Time.time>=reload+1f) {
				if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
					GameObject tempPrefab=Instantiate(bullet) as GameObject;
					tempPrefab.transform.position=new Vector3(jumper.transform.position.x, jumper.transform.position.y, jumper.transform.position.z+0.15f);
					reload=Time.time;
				}
			}
		}
	}
}
