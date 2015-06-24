using UnityEngine;
using System.Collections;

public class SelectorBehavior : MonoBehaviour {

	// PUBLIC PROPERTIES
	public float RotationSpeed = 100.0f;
	public float SelectorSize = 7.0f;
	public static bool  EnableSelector { get; set; }
	public static GameObject CurrentSelection { get; set; }
	
	// PRIVATE VARIABLES
	// Current SelectorAngle
	private float _selectorAngle = 0.0f;
	private GameObject playerReference;
	// Use this for initialization
	void Start () {
		EnableSelector = false;
		playerReference = GameObject.Find ("First Person Controller").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (EnableSelector) {	
			base.renderer.enabled = true;
			base.transform.localScale = new Vector3 ((CurrentSelection.collider.bounds.size.x / 10.0f) * SelectorSize, 0f, (CurrentSelection.collider.bounds.size.x / 10.0f) * SelectorSize);
			base.transform.position = CurrentSelection.transform.position + (new Vector3 (playerReference.transform.forward.x, 0f, playerReference.transform.forward.z) * .2f);
			base.transform.LookAt (playerReference.transform.position);
			base.transform.Rotate (90f, 0f, 0f);
			base.transform.RotateAround (base.transform.position, base.transform.up, _selectorAngle);
			_selectorAngle += RotationSpeed * Time.deltaTime;
		} else {
			if(base.renderer.enabled)
				base.renderer.enabled = false;
		}
	}
}
