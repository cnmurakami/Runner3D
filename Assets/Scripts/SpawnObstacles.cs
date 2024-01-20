using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Generates up to 3 obstacles, ramps or holes at the same line
/// If a hole or a ramp is generated, no obstacles will be generated
/// </summary>
public class SpawnObstacles : MonoBehaviour {
	[Tooltip("Obstacle that is possible to overpass")]
	public GameObject pas1;
	[Tooltip("Obstacle that is possible to overpass")]
	public GameObject pas2;
	[Tooltip("Obstacle that is possible to overpass")]
	public GameObject pas3;
	[Tooltip("Obstacle that is possible to overpass")]
	public GameObject pas4;
	[Tooltip("Obstacle that is possible to overpass")]
	public GameObject pas5;
	[Tooltip("Obstacle that is impossible to overpass")]
	public GameObject non1;
	[Tooltip("Obstacle that is possible to overpass")]
	public GameObject non2;
	[Tooltip("Obstacle that is possible to overpass")]
	public GameObject non3;
	[Tooltip("Obstacle that is possible to overpass")]
	public GameObject non4;
	[Tooltip("Object to destroy ground tiles to make a hole")]
	public GameObject destroyer;
	[Tooltip("Ramp")]
	public GameObject ramp;
	private bool canBreak=false;

	//List of Passable(0) or Non Passable(1) to be generated (range of 3)
	private int[] spawn;
	//List of Obstacles to be spawned (range of 3)
	private GameObject[] tempPrefab;
	//Checks if it is possible to pass somehow between the three obstacles
	private bool isPassable=false;
	//List of position of each obstacle
	private float[] pos;
	//General random generator
	private int chance;
	//Chance to spawn a Non Passable Object at a determined position of the list
	private int nonPassableChance;
	private int[] sorted;

	/// <summary>
	/// Generates random obstacles to this Game Object's Z position
	/// </summary>
	void Start() {
		nonPassableChance=60;
		tempPrefab=new GameObject[3];
		spawn=new int[3];
		pos=new float[3];
		sorted=new int[3];
		pos[0]=-0.5f;
		pos[1]=0f;
		pos[2]=0.5f;
		/// Randomly generates 0 or 1 values for the 3 possible obstacles positions
		/// If 0, there will be no obstacle or a passable obstacle at the list position
		/// If 1, there will be a impossible to pass obstacle at the list position
		/// For the game to be playable, at least one 0 value has to be reached
		while (true) {
			for (int i=0; i<spawn.Length; i++) {
				chance=Random.Range(0, 100);
				if (chance<=nonPassableChance) {
					spawn[i]=1;
				}
				else {
					spawn[i]=0;
					isPassable=true;
				}
			}
			if (isPassable==true) {
				break;
			}
		}


		///Checks if a hole or ramp will be generated instead of obstacles
		for (int i=0; i<spawn.Length; i++) {
			chance=Random.Range(0, 101);
			sorted[i]=chance;
			if (chance>94 && chance<=97) {
				Spawn(tempPrefab[i], ramp, pos[i]);
				canBreak=true;
				break;
			}
			if (chance>97) {
				Spawn(tempPrefab[i], destroyer, 0);
				canBreak=true;
				break;
			}
		}
		///If no hole or ramp is generated, creates a obstacle to each of the 3 positions based on the 0 or 1 value and using the generated sorting number at the past 'for';
		///Note that Random.Range for Passable obstacles does not start with 0 because it may not spawn any obstacle as well
		if (canBreak==false) {
			for (int i=0; i<spawn.Length; i++) {
				if (spawn[i]==0) {
					float percentage=94/6;
					if (sorted[i]>percentage*1 && sorted[i]<=percentage*2) {
						Spawn(tempPrefab[i], pas1, pos[i]);
					}
					if (sorted[i]>percentage*2 && sorted[i]<=percentage*3) {
						Spawn(tempPrefab[i], pas2, pos[i]);
					}
					if (sorted[i]>percentage*3 && sorted[i]<=percentage*4) {
						Spawn(tempPrefab[i], pas3, pos[i]);
					}
					if (sorted[i]>percentage*4 && sorted[i]<=percentage*5) {
						Spawn(tempPrefab[i], pas4, pos[i]);
					}
					if (sorted[i]>percentage*5 && sorted[i]<=percentage*6) {
						Spawn(tempPrefab[i], pas5, pos[i]);
					}
				}
				else if (spawn[i]==1) {
					float percentage=100/4;
					if (sorted[i]<=percentage*1) {
						Spawn(tempPrefab[i], non1, pos[i]);
					}
					if (sorted[i]>percentage*1 && sorted[i]<=percentage*2) {
						Spawn(tempPrefab[i], non2, pos[i]);
					}
					if (sorted[i]>percentage*2 && sorted[i]<=percentage*3) {
						Spawn(tempPrefab[i], non3, pos[i]);
					}
					if (sorted[i]>percentage*3 && sorted[i]<=percentage*4) {
						Spawn(tempPrefab[i], non4, pos[i]);
					}
				}
			}
		}
	}

	/// <summary>
	/// Generates prefabs, stores then at correct position and add them as a child of this gameObject
	/// If the Spawner is located at the second floor, special conditions are given to ramp and destroyers
	/// </summary>
	/// <param name="temp">temPrefab list position</param>
	/// <param name="obs">Obstacle to be instantiated</param>
	/// <param name="pos">Position list position</param>
	void Spawn(GameObject temp, GameObject obs, float pos) {
		if (this.transform.position.y>0.5f && (obs==destroyer || obs==ramp)) {
			obs=destroyer;
			temp=Instantiate(obs) as GameObject;
			temp.transform.position=new Vector3(pos, this.transform.position.y, this.transform.position.z);
			temp.transform.parent=this.transform;
			temp=Instantiate(obs) as GameObject;
			temp.transform.position=new Vector3(pos, this.transform.position.y, this.transform.position.z);
			temp.transform.position=new Vector3(transform.position.x, transform.position.y, transform.position.z+PlayerController.block);
		}
		else {
			temp=Instantiate(obs) as GameObject;
			temp.transform.position=new Vector3(pos, this.transform.position.y, this.transform.position.z);
			temp.transform.parent=this.transform;
		}
	}
}
