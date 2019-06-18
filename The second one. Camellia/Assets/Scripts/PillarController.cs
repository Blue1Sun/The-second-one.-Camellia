using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarController : MonoBehaviour {

	public GameObject cam;
	public LocationLoading locLoad;

	public void CutSceneStart(){
		cam.GetComponent<CameraController> ().enabled = false;
		cam.transform.position = new Vector3 (322, 28.68f, -10);

		locLoad.CutSceneStart ();
	}

	public void CutSceneEnd(){
		cam.GetComponent<CameraController> ().enabled = true;
		Vector3 charPos = GameObject.Find ("Character").transform.position;
		cam.transform.position = new Vector3 (charPos.x, charPos.y + 2.33f, -10);

		locLoad.CutSceneEnd ();
		GetComponent<Animator> ().SetBool ("falled", true);
	}
}
