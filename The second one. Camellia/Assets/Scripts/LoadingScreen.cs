using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

	public Slider loadingProgressBar;
	public string loadingSceneName;

	void Start () {
		StartCoroutine (Loading ());	
	}

	IEnumerator Loading(){
		AsyncOperation async = SceneManager.LoadSceneAsync (loadingSceneName);

		while (!async.isDone) {
			loadingProgressBar.value = Mathf.Clamp01 (async.progress / 0.9f);
			yield return null;
		}
	}
}