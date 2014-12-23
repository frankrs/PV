using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GenButton : MonoBehaviour {


	public Text text;
	public string buttonString;
	public StartMenuManager smManager;

	public void SetUpButton(string s, StartMenuManager smm){
		buttonString = s;
		text.text = buttonString;
		smManager = smm;
	}


	public void ClickecMe(){
		smManager.SelectServerPano(buttonString);
	}
}
