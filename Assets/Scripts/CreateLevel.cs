using UnityEngine;
using System.Collections;

/// <summary>
/// Called once per game to create level and obstacles spawner at desired distances
/// </summary>
public class CreateLevel : MonoBehaviour {
	[Tooltip("Obstacles spawner gameObject")]
	public GameObject spawner;
	[Tooltip("Finish Line prefab")]
	public GameObject finishLine;
	[Tooltip("Distance between obstacles in blocks")]
	public int defaultDistance;
	[Tooltip("Level size")]
	public int defaultMaxDistance;
	[Tooltip("Generated Ground Prefab")]
	public GameObject ground;
	[Tooltip("Generated Wall Prefab")]
	public GameObject GroundWall;
	[Tooltip("Single Wall Prefab")]
	public GameObject wall;

	protected float maxDist;
	protected float curDist;

	[Tooltip("Generated automatically based on difficult")]
	public int distance;
	[Tooltip("Generated automatically based on difficult")]
	public int maxDistance;

	[Tooltip("Distance between the start point and the first obstacle")]
	public float start;

	private float currentDistance;
	private float blocks;

	/// <summary>
	/// Changes the distances values based on difficult and creates a obstacle spawner at every distance value and creates a Finish line at maxDistance
	/// </summary>
	void Start() {
		if (DifficultSelect.difficult==0) {
			distance=defaultDistance+3;
			maxDistance=defaultMaxDistance-50;
		}
		if (DifficultSelect.difficult==1) {
			distance=defaultDistance;
			maxDistance=defaultMaxDistance;
		}
		if (DifficultSelect.difficult==2) {
			distance=defaultDistance-1;
			maxDistance=defaultMaxDistance+50;
		}
		if (DifficultSelect.difficult==3) {
			distance=defaultDistance-2;
			maxDistance=defaultMaxDistance+100;
		}

		GenerateGround();

		blocks=distance*PlayerController.block;
		currentDistance=this.transform.position.z+start;
		while (currentDistance<maxDistance-blocks*2) {
			GameObject tempPrefab=Instantiate(spawner) as GameObject;
			tempPrefab.transform.position=new Vector3(this.transform.position.x, this.transform.position.y, currentDistance);
			currentDistance+=blocks;
		}
		currentDistance=this.transform.position.z+start;
		while (currentDistance<maxDistance-blocks*2) {
			GameObject tempPrefab=Instantiate(spawner) as GameObject;
			tempPrefab.transform.position=new Vector3(this.transform.position.x, this.transform.position.y+PlayerController.block*3-(PlayerController.block/2), currentDistance);
			currentDistance+=blocks;
		}
		GameObject finish=Instantiate(finishLine) as GameObject;
		finish.transform.position=new Vector3(this.transform.position.x, this.transform.position.y, maxDistance);
	}

	/// <summary>
	/// Generates grounds, ceiling and walls.
	/// </summary>
	void GenerateGround() {
		curDist=this.transform.position.x-PlayerController.block;
		maxDist=(this.transform.position.x+maxDistance);
		while (curDist<=maxDist+PlayerController.block*2) {
			GameObject tempGround2=Instantiate(ground) as GameObject;
			tempGround2.transform.position=new Vector3(0, 0, curDist);
			curDist+=PlayerController.block;
		}
		curDist=this.transform.position.x-PlayerController.block;
		while (curDist<=maxDist+PlayerController.block*2) {
			GameObject tempWall=Instantiate(GroundWall) as GameObject;
			tempWall.transform.position=new Vector3(0, 0, curDist);
			curDist+=PlayerController.block*3+0.12f;
		}

		GameObject tempWallFinal=Instantiate(wall) as GameObject;
		tempWallFinal.transform.position=new Vector3(-PlayerController.block-0.055f, 0, maxDist+PlayerController.block*2.5f);
		tempWallFinal.transform.Rotate(0, 0, -90);
		GameObject tempWallFinalUp=Instantiate(wall) as GameObject;
		tempWallFinalUp.transform.position=new Vector3(-PlayerController.block-0.055f, PlayerController.block*3-(PlayerController.block/2), maxDist+PlayerController.block*2.5f);
		tempWallFinalUp.transform.Rotate(0, 0, -90);
	}
}
