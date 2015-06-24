using UnityEngine;
using System.Collections;

public class MenuScriptPick : MenuScript {
	private TextMesh[] _textRenderers;
	private GameObject _parentMenu;
	private Vector3	_FPSPosition;
	private GameObject _FPSObject;
	void Start()
	{
		this._parentMenu = GameObject.Find ("Game Menu").gameObject;
		this._FPSObject = GameObject.Find ("Main Camera").gameObject;
		this._textRenderers = this.transform.GetComponentsInChildren<TextMesh>();
	}
	
	void Update () {
		if (gameObject == CurrentSelection)
		{
			this._textRenderers[0].color = Color.red;
		}
		else
		{
			this._textRenderers[0].color = Color.white;
		}

		if (CurrentLogic == PickItem) {
			if(SelectorBehavior.CurrentSelection!=null)
			{
				// Some feedback that you actually did something
				this._textRenderers[0].color = Color.green;
				// Disable Collisions
				if(SelectorBehavior.CurrentSelection.collider.enabled)
					SelectorBehavior.CurrentSelection.collider.enabled = false;
				if(SelectorBehavior.CurrentSelection.rigidbody.useGravity)
					SelectorBehavior.CurrentSelection.rigidbody.useGravity = false;

				// Translate the position based on where the player moves
				SelectorBehavior.CurrentSelection.transform.position = this._FPSObject.transform.position - _FPSPosition;
			}
			else{
				CurrentLogic = NoItem;
			}
		}
	}
	public override void MenuScriptRun()
	{
		if (CurrentLogic == NoItem) {
			CurrentLogic = PickItem;
			// Save the direction vector
			if (SelectorBehavior.CurrentSelection != null) {
				_FPSPosition = this._FPSObject.transform.position - SelectorBehavior.CurrentSelection.transform.position;
			}
			else
			{
				CurrentLogic = NoItem;
				this._parentMenu.SetActive (false);
			}

		}
	}
}
