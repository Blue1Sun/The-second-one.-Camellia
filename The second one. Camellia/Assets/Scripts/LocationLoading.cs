using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationLoading : MonoBehaviour {

	public GameObject[] parts = new GameObject[13];

	private Vector3 playerPos;
	//TODO: place borders and location numbers in matrix

	void Start(){
		playerPos = new Vector3 (CharController.startPosX, CharController.startPosY, 0);
		if(LoadingNeeded(100, -30))
			LoadPart (4);
		if (LoadingNeeded (175, -30)) {
			LoadPart (4);
			LoadPart (7);
		}
		if (LoadingNeeded (0, -5)) {
			LoadPart (1);
			LoadPart (3);
		}
		if (LoadingNeeded (275, 20)) {
			LoadPart (5);
			LoadPart (6);
		}
		if (LoadingNeeded (435, 50)) {
			LoadPart (9);
			LoadPart (10);
		}
		if (LoadingNeeded (310, -15)) {
			LoadPart (8);
			LoadPart (11);
		}
	}

	void Update(){
		playerPos = GameObject.Find ("Character").GetComponent<Transform> ().position;
		if(LoadingNeeded(130, -30))
			LoadPart (7, 0, 130, -30);
		if(LoadingNeeded(185, -5))
			LoadVertPart (5, 4, 185, -5);
		if (LoadingNeeded (170, 12))
			LoadHorizPart (2, 7, 170, 12);
		if(LoadingNeeded(90, 12))
			LoadHorizPart (1, 5, 90, 12);
		if(LoadingNeeded(50, 15))
			LoadHorizPart (3, 2, 50, 15);
		if(LoadingNeeded(55, -2))
			LoadHorizPart (1, 4, 55, -2);
		if(LoadingNeeded(130, -5))
			LoadHorizPart (3, 7, 130, -5);
		if(LoadingNeeded(205, 23))
			LoadHorizPart (7, 6, 205, 23);
		if(LoadingNeeded(285, 23))
			LoadHorizPart (5, 9, 285, 23);
		if(LoadingNeeded(365, 50))
			LoadHorizPart (6, 10, 365, 50);
		if(LoadingNeeded(450, 50))
			LoadHorizPart (9, 12, 450, 50);
		if(LoadingNeeded(520, 50))
			LoadHorizPart (10, 13, 520, 50);
		if (LoadingNeeded (318, 15)) {
			LoadVertPart (0, 8, 318, 15);
			LoadVertPart (0, 11, 318, 15);
		}
		if (LoadingNeeded (318, -2)) {
			LoadVertPart (6, 0, 318, -2);
			LoadVertPart (9, 0, 318, -2);
		}
		if (LoadingNeeded (300, -40)) {
			LoadHorizPart (7, 11, 300, -40);
		}
		if (LoadingNeeded (190, -40)) {
			LoadHorizPart (4, 8, 190, -40);
		}
	}

	// Functions called from animator
	public void CutSceneStart(){
		LoadPart (6);
		LoadPart (9);
	}
	public void CutSceneEnd(){
		Destroy (GameObject.Find ("6"));
		Destroy (GameObject.Find ("9"));
	}

	bool LoadingNeeded(int minPosX, int minPosY){
		return (playerPos.x > minPosX && playerPos.x < minPosX + 20 && playerPos.y > minPosY - 10 && playerPos.y < minPosY + 10);
	}

	bool InSmallBounds(int minPosX, int minPosY){
		return (playerPos.x > minPosX && playerPos.x < minPosX + 10 && playerPos.y > minPosY && playerPos.y < minPosY + 10);
	}

	void LoadPart (int newLoc){
		GameObject part = (GameObject)Instantiate (parts [newLoc - 1], transform);
		part.name = newLoc.ToString ();
	}

	void LoadPart(int newLoc, int oldLoc, int minPosX, int minPosY){
		if (GameObject.Find (newLoc.ToString()) == null && InSmallBounds(minPosX, minPosY)) {
			if (newLoc != 0) {
				LoadPart (newLoc);
			}
			if (oldLoc != 0 && GameObject.Find (oldLoc.ToString()) != null)
				Destroy (GameObject.Find (oldLoc.ToString()));
		}
	}

	void LoadVertPart(int newLoc, int oldLoc, int minPosX, int minPosY){
		LoadPart (newLoc, oldLoc, minPosX, minPosY);
		LoadPart (oldLoc, newLoc, minPosX, minPosY - 10);
	}

	void LoadHorizPart(int newLoc, int oldLoc, int minPosX, int minPosY){
		LoadPart (newLoc, oldLoc, minPosX, minPosY);
		LoadPart (oldLoc, newLoc, minPosX + 10, minPosY);
	}
}
