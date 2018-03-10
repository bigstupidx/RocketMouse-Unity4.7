using UnityEngine;
using System.Collections;

public class ParallaxScroll : MonoBehaviour {

	#region vars
	//quad renderers ref
	public Renderer background;
	public Renderer foreground;

	//speeds for parallax speeds
	public float backgroundSpeed = 0.02f;
	public float foregroundSpeed = 0.06f;

	//offset ref, to be passed in
	public float offset = 0;
	#endregion

	#region onUpdate
	void Update () {
		//calculate offset values for parallax layers
		//by multiplying speed by passed in offset value
		float backgroundOffset = offset * backgroundSpeed;
		float foregroundOffset = offset * foregroundSpeed;

		//apply offset to textures on parallax layers
		background.material.mainTextureOffset = new Vector2(backgroundOffset, 0);
		foreground.material.mainTextureOffset = new Vector2(foregroundOffset, 0);
	}
	#endregion
}
