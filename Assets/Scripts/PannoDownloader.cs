using UnityEngine;
using System.Collections;

public class PannoDownloader : MonoBehaviour {

	public string url;
	public 

	// Use this for initialization
	void Start () {
		StartCoroutine("DownloadPanno");
	}
	

	IEnumerator DownloadPanno (){
		WWW www = new WWW(url);
		yield return www;
		renderer.material.mainTexture = www.texture;
	}


}
