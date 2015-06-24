using UnityEngine;
using System.Collections;

public class MenuScriptDelete : MenuScript {
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
		CurrentLogic = DeleteItem;
		if(SelectorBehavior.CurrentSelection!=null) 
			SelectorBehavior.CurrentSelection.SetActive (false);  // "Delete" the object
		CurrentLogic = NoItem;
		this._parentMenu.SetActive (false);
	}
}
