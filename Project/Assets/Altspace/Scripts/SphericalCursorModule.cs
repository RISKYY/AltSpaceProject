﻿using UnityEngine;

public class SphericalCursorModule : MonoBehaviour {
	// Sphere radius to project cursor onto if no raycast hit.
	public float SphereRadius = 50.0f;

	// This is a sensitivity parameter that should adjust how sensitive the mouse control is.
	public float Sensitivity;

	// This is a scale factor that determines how much to scale down the cursor based on its collision distance.
	public float DistanceScaleFactor;
	
	// This is the layer mask to use when performing the ray cast for the objects.
	// The furniture in the room is in layer 8, everything else is not.
	private const int ColliderMask = (1 << 8);

	// This is the Cursor game object. Your job is to update its transform on each frame.
	private GameObject Cursor;

	private GameObject SelectorDisk;

	private Vector3 SelectedDiskObj;

	// This is the Cursor mesh. (The sphere.)
	private MeshRenderer CursorMeshRenderer;

	// This is the scale to set the cursor to if no ray hit is found.
	private Vector3 DefaultCursorScale = new Vector3(10.0f, 10.0f, 10.0f);

	// Maximum distance to ray cast.
	private const float MaxDistance = 100.0f;

	// Coordinates in Spherical Coordinates of the cursor
	private SphericalCoordinates _SphereCoords;

    void Awake() {
		this._SphereCoords = new SphericalCoordinates(SphereRadius, Mathf.PI/2.0f, 0f, 0f, SphereRadius);
		Cursor = transform.Find("Cursor").gameObject;
		CursorMeshRenderer = Cursor.transform.GetComponentInChildren<MeshRenderer>();
        CursorMeshRenderer.renderer.material.color = new Color(0.0f, 0.8f, 1.0f);
    }	

	void Update()
	{
		// Handle mouse movement to update cursor position.
		this._SphereCoords.polar 	 -= this.Sensitivity * Input.GetAxis ("Mouse X");
		this._SphereCoords.elevation += this.Sensitivity * Input.GetAxis ("Mouse Y");

		// Perform ray cast to find object cursor is pointing at.
		// Raycast from Camera Position, 
		// Direction is absolute cursor position - camera position
		// Max Distance from private const
		// Collider Mask from private const
		RaycastHit cursorHit;
		Physics.Raycast(base.transform.position, 
		                base.transform.TransformPoint(this._SphereCoords.toCartesian)-base.transform.position,
		                out cursorHit, MaxDistance, ColliderMask);

		// Update highlighted object based upon the raycast.
		if (cursorHit.collider != null)
		{
			Selectable.CurrentSelection = cursorHit.collider.gameObject;
			// Update cursor transform.
			float tempScaler = (cursorHit.distance * DistanceScaleFactor + 1.0f) / 2.0f;
			this.Cursor.transform.localScale = new Vector3(tempScaler, tempScaler, tempScaler);
			this.Cursor.transform.position = cursorHit.point;

		}
		else
		{
			Selectable.CurrentSelection = null;
			// Update cursor transform.
			this.Cursor.transform.localScale = DefaultCursorScale;
			this.Cursor.transform.localPosition = this._SphereCoords.toCartesian;

		}

	}
}
