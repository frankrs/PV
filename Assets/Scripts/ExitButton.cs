using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {
	

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("escape")){
			if(Application.loadedLevel == 2 || Application.loadedLevel == 3){
				Application.LoadLevel(1);
			}
			else{
			Application.Quit();
			}
		}
	}
}
