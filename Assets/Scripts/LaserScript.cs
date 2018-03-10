using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

	#region vars
	//sprite refs
	public Sprite laserOnSprite;
	public Sprite laserOffSprite;
	
	//timing parameters
	public float interval = 0.5f;
	public float rotationSpeed = 0.0f;
	
	//on/off flag and tracker time
	private bool isLaserOn = true;
	private float timeUntilNextToggle;
	#endregion

	#region onStart
	void Start () {
		//init tracker time with interval time
		timeUntilNextToggle = interval;
	}
	#endregion
	
	#region onUpdate
	void Update () {
	
	}
	#endregion

	#region onFixedUpdate
	void FixedUpdate () {
		//decrease tracker time with fixedDeltaTime
		//time between fixed frames
		timeUntilNextToggle -= Time.fixedDeltaTime;

		//if tracker time is up: time to toggle
		//laser will be on/off for the same amount of time till switching
		if (timeUntilNextToggle <= 0) {
			//revert laser on/off flag
			isLaserOn = !isLaserOn;
			//map laser flag to collider
			collider2D.enabled = isLaserOn;
			//get renderer comp ref and
			//type cast this object's renderer as SpriteRenderer
			//due to UnityEngine.Renderer cannot be converted to UnityEngine.SpriteRenderer
			SpriteRenderer spriteRenderer = ((SpriteRenderer)this.renderer);
			//switch sprite image
			spriteRenderer.sprite = isLaserOn ? laserOnSprite : laserOffSprite;
			//reset tracker time to interval time
			timeUntilNextToggle = interval;
		}
		
		//keep instance rotating
		transform.RotateAround(
			//rotation pivot point: use object's position: (0,0,0)
			transform.position,
			//rotation direction: Vector3(0,0,1)
			Vector3.forward,
			//rotation amount in
			//angle degrees: elapsed time * speed
			rotationSpeed * Time.fixedDeltaTime);
	}
	#endregion
}
