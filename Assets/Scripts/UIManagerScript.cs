using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

	#region vars
	//animator refs
	public Animator startButton;
	public Animator settingsButton;
	public Animator dialog;
	public Animator contentPanel;
	public Animator gearImage;
	#endregion

	#region onStart
	void Start () {
		//move content panel down on start
		//------------------------------------------------------------------------
		//get contentPanel(animator)'s gameobject's transform
		//and type cast it as RectTransform
		RectTransform transform = contentPanel.gameObject.transform as RectTransform;
		//get content panel's anchor pos as target pos
		//so it is masked
		Vector2 position = transform.anchoredPosition;
		//update target pos with minus its height
		//and assign it back to content panel
		position.y -= transform.rect.height;
		transform.anchoredPosition = position;
		//set initial hidden flag to false to match the hidden state
		contentPanel.SetBool("isHidden", true);
	}
	#endregion
	
	#region onUpdate
	void Update () {
		
	}
	#endregion

	#region methods
	//load game level
	//------------------------------------------------------------------------
	public void StartGame() {
		Application.LoadLevel("RocketMouse");
	}

	//open settings menu
	//------------------------------------------------------------------------
	public void OpenSettings() {
		//trigger out anims for main menu buttons
		startButton.SetBool("isHidden", true);
		settingsButton.SetBool("isHidden", true);

		//enable dialog animator and trigger in anim
		//for settings panel
		dialog.enabled = true;
		dialog.SetBool("isHidden", false);
	}

	//close settings menu
	//------------------------------------------------------------------------
	public void CloseSettings() {
		//trigger in anims for main menu buttons
		startButton.SetBool("isHidden", false);
		settingsButton.SetBool("isHidden", false);

		//trigger out anim for settings panel
		dialog.SetBool("isHidden", true);
	}

	//toggle sliding menu
	//------------------------------------------------------------------------
	public void ToggleMenu() {
		//enable animators
		contentPanel.enabled = true;
		gearImage.enabled = true;

		//get current hidden flag value from animator
		bool isHidden = contentPanel.GetBool("isHidden");
		//set animators hidden flag value with toggled value
		contentPanel.SetBool("isHidden", !isHidden);
		gearImage.SetBool("isHidden", !isHidden);
	}

	//toggle audio proxy fn to playerSettings
	//------------------------------------------------------------------------
	public void toggleAudio(bool state) {
		GameObject pSettingsObj = GameObject.Find("PlayerSettings");
		if (pSettingsObj) {
			PlayerSettings pSettings = pSettingsObj.GetComponent<PlayerSettings>();
			if (pSettings) {
				pSettings.toggleAudio(state);
			}
		}
	}
	
	//set volume proxy fn to playerSettings
	//------------------------------------------------------------------------
	public void setVolume(float vol) {
		GameObject pSettingsObj = GameObject.Find("PlayerSettings");
		if (pSettingsObj) {
			PlayerSettings pSettings = pSettingsObj.GetComponent<PlayerSettings>();
			if (pSettings) {
				pSettings.setVolume(vol);
			}
		}
	}
	#endregion
}
