using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanButton : MonoBehaviour {

	public PanoNode panNode;
	
	public StartMenuManager menu;
	
	public Text t;
	
	// Use this for initialization
	public void DrawButton () {
		t = transform.Find("Text").GetComponent<Text>();
		t.text = panNode.seoName;
	}
	
	public void ClickedOnMe(){
		menu.PanSelect(panNode);
	}

}
