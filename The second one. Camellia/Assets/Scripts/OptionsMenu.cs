using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

	public Dropdown resolutionDropdown;
	public Slider volumeSlider;
	public Toggle fullscreenToggle;
	public Dropdown qualityDropdown;

	Resolution[] resolutions;

	void Start () {
		resolutions = Screen.resolutions;
		resolutionDropdown.ClearOptions ();

		volumeSlider.value = PlayerPrefs.GetFloat ("volume", 0.5f);

		fullscreenToggle.isOn = Screen.fullScreen;
		qualityDropdown.value = QualitySettings.GetQualityLevel ();


		List<string> options = new List<string> ();

		int currentResolutionIndex = 0; 
		for (int i = 0; i < resolutions.Length; i++) {
			string option = resolutions [i].width + " x " + resolutions [i].height;
			options.Add (option);

			if (resolutions [i].width == Screen.width && resolutions [i].height == Screen.height)
				currentResolutionIndex = i;
		}

		resolutionDropdown.AddOptions (options);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue ();
	}

	public void SetVolume(float volume) {
		PlayerPrefs.SetFloat ("volume", volume);
		FindObjectOfType<AudioSource> ().volume = PlayerPrefs.GetFloat ("volume", 0.5f);
	}

	public void SetQuality (int qualityIndex) {
		QualitySettings.SetQualityLevel (qualityIndex);
	}

	public void SetFullscreen (bool isFullscreen) {
		Screen.fullScreen = isFullscreen;
	}

	public void SetResolution (int resolutionIndex){
		Resolution resolution = resolutions [resolutionIndex];
		Screen.SetResolution (resolution.width, resolution.height, Screen.fullScreen);
	}
}
