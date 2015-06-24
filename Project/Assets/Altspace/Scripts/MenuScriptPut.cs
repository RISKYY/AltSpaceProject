using UnityEngine;
using System.Collections;

public class MenuScriptPut : MenuScript {
	private TextMesh[] _textRenderers;
	private GameObject _parentMenu;
	void Start()
	{
		this._parentMenu = GameObject.Find ("Game Menu").gameObject;
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
	}
	public override void MenuScriptRun()
	{
		if (CurrentLogic == PickItem) {
			CurrentLogic = PutItem;
			// Re-enable the collision
			if(!SelectorBehavior.CurrentSelection.collider.enabled)
				SelectorBehavior.CurrentSelection.collider.enabled = true;
			if(!SelectorBehavior.CurrentSelection.rigidbody.useGravity)
				SelectorBehavior.CurrentSelection.rigidbody.useGravity = true;
		}
		CurrentLogic = NoItem;
		this._parentMenu.SetActive (false);
	}

}
