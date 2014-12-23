using UnityEngine;
using System.Collections;

public class UploadSite : MonoBehaviour {
	

	public string url;

	public void UploadWebSite () {
		Application.OpenURL(url);
	}
}
