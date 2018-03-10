using UnityEngine;
using System.Collections;
//for using new GUI
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour {

	#region vars
	//public setting vars to share across the game
	public bool audioEnabled;
	public float volume;

	//last volume value right before being disabled
	public float lastVolume;
	#endregion

	#region onStart
	void Start () {
		//set init values
		audioEnabled = true;
		volume = 1.0f;

		//preserve this gameobject across scenes
		DontDestroyOnLoad(this);
	}
	#endregion

	#region methods
	//toggle audio
	//------------------------------------------------------------------------
	public void toggleAudio(bool state) {
		//exit on unchanged value
		if (audioEnabled == !state) {
			return;
		}

		//pass UI control value to settings var
		audioEnabled = !state;

		//if toggle to false
		//store current volume as last volume
		if (!audioEnabled) {
			lastVolume = (volume == 0.0f) ? 1.0f : volume;
		}

		//sync volume UI control to toggle UI control
		GameObject sldObj = GameObject.Find("sdr_volume");
		if (sldObj) {
			//update slider
			Slider volumeSlider = sldObj.GetComponent<Slider>();
			//if toggle to true and has valid last volume
			if (audioEnabled && (lastVolume > 0.0f)) {
				//set slider control to last volume
				volumeSlider.value = lastVolume;
			}
			//otherwise set to 0
			else {
				volumeSlider.value = 0.0f;
			}
		}
	}

	//set volume
	//------------------------------------------------------------------------
	public void setVolume(float vol) {
		//exit on unchanged value
		if (volume == vol) {
			return;
		}

		//pass UI control value to settings var
		volume = vol;

		//sync toggle UI control to volume UI control
		GameObject tglObj = GameObject.Find("tgl_sound");
		if (tglObj) {
			//update toggle
			Toggle volumeToggle = tglObj.GetComponent<Toggle>();
			if (volume == 0.0f) {
				volumeToggle.isOn = true;
			} else {
				volumeToggle.isOn = false;
			}
		}
	}
	#endregion

	#region handlers
	//on level loaded
	//------------------------------------------------------------------------
	void OnLevelWasLoaded(int level) {
		//refs
		GameObject mainCam = GameObject.Find("Main Camera");
		GameObject sldObj = GameObject.Find("sdr_volume");
		GameObject tglObj = GameObject.Find("tgl_sound");

		//if main camera exists
		//set volumn on main camera to stored volume value
		if (mainCam) {
			AudioSource music = mainCam.GetComponent<AudioSource>();
			music.volume = audioEnabled ? volume : 0.0f;
		}

		//if slider object exists
		//get slider UI and set its value to stored volume value
		if (sldObj) {
			Slider volumeSlider = sldObj.GetComponent<Slider>();
			volumeSlider.value = volume;
		}

		//if toggle object exists
		//get slider UI and set its value to stored volume value
		if (tglObj) {
			Toggle volumeToggle = tglObj.GetComponent<Toggle>();
			volumeToggle.isOn = !audioEnabled;
		}

		//Debug.Log ("level: " + level + ", vol: " + volume + ", audioEnabled: " + audioEnabled);
	}
	#endregion
}
