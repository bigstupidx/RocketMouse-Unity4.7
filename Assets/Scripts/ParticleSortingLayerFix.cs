using UnityEngine;
using System.Collections;

public class ParticleSortingLayerFix : MonoBehaviour {

	#region onStart
	void Start () {
		//set renderer sorting layer to player
		particleSystem.renderer.sortingLayerName = "Player";
		//set renderer sorting order to -1 to be beneath everything
		particleSystem.renderer.sortingOrder = -1;
	}
	#endregion
	
	#region onUpdate
	void Update () {
		
	}
	#endregion
}
