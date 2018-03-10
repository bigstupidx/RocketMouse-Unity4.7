using UnityEngine;
using System.Collections;

public class SettingsLoader : MonoBehaviour {

	#region onStart
	void Start () {
		//this scene will kick off the playerSettings gameobject
		//and kept in app across scenes

		//this scene is never revisited

		//load menu scene on start game
		Application.LoadLevel("MenuScene");
	}
	#endregion
}
