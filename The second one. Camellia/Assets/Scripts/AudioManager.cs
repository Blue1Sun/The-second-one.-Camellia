using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager: MonoBehaviour {
	public static AudioManager instance = null; 

	void Start () {
		// Singleton Audiosource with music
		if (instance == null) {
			instance = this;
			GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat ("volume", 0.5f);
		} else {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
}