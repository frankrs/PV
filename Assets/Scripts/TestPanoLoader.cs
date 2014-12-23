using UnityEngine;
using System.Collections;

public class TestPanoLoader : MonoBehaviour{

	// Use this for initialization
	void Start () {
		if(Pano.pic){
		renderer.material.mainTexture = Pano.pic;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
