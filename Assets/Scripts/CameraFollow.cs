using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	#region private vars
	//position offset to target
	private float distanceToTarget;
	#endregion
	
	#region public vars
	//target object ref
	public GameObject targetObject;
	#endregion
	
	#region onStart
	void Start () {
		//calculate target offset by subtracting their x positions
		distanceToTarget = transform.position.x - targetObject.transform.position.x;
	}
	#endregion
	
	#region onUpdate
	void Update () {
		//get target object's x position, camera only moves horizontally
		float targetObjectX = targetObject.transform.position.x;

		//copy target object's x position to camera
		//set camera's position's x-component without affecting y-component
		Vector3 newCameraPosition = transform.position;
		newCameraPosition.x = targetObjectX + distanceToTarget;
		transform.position = newCameraPosition;
	}
	#endregion
}
