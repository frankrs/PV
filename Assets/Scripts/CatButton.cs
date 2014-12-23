using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CatButton : MonoBehaviour {

	public CatagoryNode catNode;

	public StartMenuManager menu;

	public Text t;

	// Use this for initialization
	public void DrawButton () {
		t = transform.Find("Text").GetComponent<Text>();
		t.text = catNode.name;
	}
	
	public void ClickedOnMe(){
		menu.CatSelect(catNode);
	}
}
