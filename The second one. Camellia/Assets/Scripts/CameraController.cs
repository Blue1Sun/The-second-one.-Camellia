using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float yCamIndent = 3.1f;

	private bool[] xyMoving = new bool[2] {false, false};
	private Vector3 playerPos;

	// Borders for horizontal camera movement
	private float[,] horizMove = new float[12, 4] {
		{ -26, -21.5f, 103, 192 },
		{ 0, 4, 179.1f, 190 },
		{ 13.5f, 22, 50.35f, 190 },
		{ -1, 1, 45, 55 },
		{ -1, 8, 9.97f, 45 },
		{ -1, 8, 55, 153.5f },
		{ 24, 30, 196, 344.4f },
		{ -15, -10, 302, 318 },
		{ -38, -34, 192, 321 },
		{ -34, -30, 265, 305 },
		{ 35, 39, 353, 363.6f },
		{ 50, 65, 350, 606 },
	};

	// Borders for vertical camera movement
	private float[,] vertMove = new float[5, 4] {
		{ -35.5f, 26, 188, 196 },
		{ 1, 14.5f, 30, 55 },
		{ -36, 24, 318, 330 },
		{ -47, -40, 274, 285 },
		{ 25, 54, 340, 352 },
	};

	void Start () {
		this.transform.position = new Vector3 (CharController.startPosX, CharController.startPosY + yCamIndent, -10);	
	}
		

	void Update () {
		playerPos = GameObject.Find ("Character").GetComponent<Transform> ().position;
		Vector3 cameraPos = this.transform.position;

		// Horizontal moving
		for (int i = 0; i < horizMove.GetLength (0) && !xyMoving [0]; i++)
			if (playerPos.y > horizMove [i, 0] && playerPos.y < horizMove [i, 1] && playerPos.x > horizMove [i, 2] && playerPos.x < horizMove [i, 3]) {
				xyMoving [0] = true;
				cameraPos.x = playerPos.x;
			}

		// Vertical moving
		if (!xyMoving [0])
			for (int i = 0; i < vertMove.GetLength (0) && !xyMoving [1]; i++)
				if (playerPos.y > vertMove [i, 0] && playerPos.y < vertMove [i, 1] && playerPos.x > vertMove [i, 2] && playerPos.x < vertMove [i, 3]) {			
					xyMoving [1] = true;
					cameraPos.y = playerPos.y + yCamIndent;
				}

		xyMoving = new bool[2]{ false, false };

		this.transform.position = cameraPos;	
	}

}
