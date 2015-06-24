using UnityEngine;
using System.Collections;

public class SelectorBehavior : MonoBehaviour {

	// PUBLIC PROPERTIES
	public float RotationSpeed = 100.0f;
	public float SelectorSize = 7.0f;
	public float AnimationSpeed = 1.4f;
	public static bool  EnableSelector { get; set; }
	public static GameObject CurrentSelection { get; set; }
	public static int AnimState { get; set; }
	// PRIVATE VARIABLES
	// Current SelectorAngle
	private float _selectorAngle = 0.0f;
	private GameObject playerReference;

	// Use this for initialization
	void Start () {
		AnimState = 0;
		EnableSelector = false;
		playerReference = GameObject.Find ("First Person Controller").gameObject;
		AnimState = 0; //Not Visible
	}
	
	// Update is called once per frame
	void Update () {
		if (EnableSelector) {	
			base.renderer.enabled = true;
			// This is required so it does not continually resize the indicator
			if(AnimState == 1)
			{
				if (base.transform.localScale.x < (CurrentSelection.collider.bounds.size.x/10.0f) * SelectorSize)
				{
					base.transform.localScale = base.transform.localScale + new Vector3(AnimationSpeed*Time.deltaTime,
					                                                                    AnimationSpeed*Time.deltaTime,
					                                                                    AnimationSpeed*Time.deltaTime);
				}
				else
					AnimState = 2;
			}
			if(AnimState == 0) // has not grown yet;
			{
				AnimState = 1; // Growing
				base.transform.localScale = new Vector3(0f,0f,0f);
			}

			// places the position of the indicator slightly behind the object from the player's perspective
			base.transform.position = CurrentSelection.transform.position + (new Vector3 (playerReference.transform.forward.x, playerReference.transform.forward.y, playerReference.transform.forward.z) * .2f);
			//These are the rotations in order for the plane to always appear to look at the player but still rotate in a cirlce
			base.transform.LookAt (playerReference.transform.position);
			base.transform.Rotate (90f, 0f, 0f);
			base.transform.RotateAround (base.transform.position, base.transform.up, _selectorAngle);
			_selectorAngle += RotationSpeed * Time.deltaTime;
		} else {
			if(base.renderer.enabled)
				base.renderer.enabled = false;
			AnimState = 0; //Not Visible
		}
	}
}
