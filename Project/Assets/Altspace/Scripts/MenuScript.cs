using UnityEngine;
using System.Collections;

public abstract class MenuScript : MonoBehaviour {
	public const string NoItem = "NO ITEM";
	public const string DeleteItem = "DELETE ITEM";
	public const string PickItem = "PICK ITEM";
	public const string PutItem = "PUT ITEM";
	public static GameObject CurrentSelection { get; set; }
	public static string CurrentLogic { get; set; }
	abstract public void MenuScriptRun();
}
