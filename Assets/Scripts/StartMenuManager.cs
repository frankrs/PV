
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Prime31;

using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;



public class StartMenuManager : MonoBehaviour {



	public List<CatagoryNode> cats;

	public RawImage image;

	public string url;
	
	public XmlDocument xml;
	
	public List<GameObject> buttons;

	public GameObject loadButton;

	public Transform searchMenu;
	public GameObject genButton;
	

	void Start()
	{
		// kick off the TTS system so it is ready for use later
		EtceteraAndroid.initTTS();
		Samples();
	}

	void OnEnable()
	{
		// Listen to the texture loaded methods so we can load up the image on our plane
		EtceteraAndroidManager.albumChooserSucceededEvent += textureLoaded;
		EtceteraAndroidManager.photoChooserSucceededEvent += textureLoaded;
	}
	
	
	void OnDisable()
	{
		EtceteraAndroidManager.albumChooserSucceededEvent -= textureLoaded;
		EtceteraAndroidManager.photoChooserSucceededEvent -= textureLoaded;
	}





	public void Samples(){
		StartCoroutine("GetXML");
	}


	IEnumerator GetXML() {
		xml = new XmlDocument();
		WWW www = new WWW(url + "panos/PanoramaList.xml");
		yield return www;
		xml.LoadXml (www.text);
		SetUpButtons();
	}

//	void SetUpButtons () {
//		foreach(XmlElement node in xml.SelectNodes("Panos/Pan")){;
//			words.Add(node.InnerText);
//			GameObject b = new GameObject();
//			b = GameObject.Instantiate(genButton,new Vector3(0f,0f,0f),Quaternion.identity) as GameObject;
//			b.transform.parent = searchMenu;
//			b.GetComponent<GenButton>().SetUpButton(node.InnerText,this);
//			buttons.Add(b);
//		}
//		
//	}

	void SetUpButtons () {
		foreach(XmlElement node in xml.SelectNodes("Panos/Category")){;
			CatagoryNode cat = new CatagoryNode();
			cat.name = node.GetAttribute("name");
			cats.Add(cat);

			MakeCatButton(cat);


		}
		foreach(CatagoryNode cat in cats){
			List <PanoNode> tempPNode = new List<PanoNode>();
			XmlNodeList xnList = xml.SelectNodes("Panos/Category[@name='"+cat.name+"']/Pan");
			//Debug.Log(xnList);
			foreach (XmlNode xn in xnList){
				PanoNode pan = new PanoNode();
				pan.name = xn.SelectSingleNode("name").InnerText;
				pan.seoName = xn.SelectSingleNode("seo_name").InnerText;
				pan.folder = xn.SelectSingleNode("folder").InnerText;
				pan.site = xn.SelectSingleNode("site").InnerText;
//				pan.latitude = float.Parse(xn.SelectSingleNode("latitude").InnerText);
//				pan.longitude = float.Parse(xn.SelectSingleNode("longitude").InnerText);
				pan.discription = xn.SelectSingleNode("description").InnerText;
				tempPNode.Add(pan);
			}
			cat.panoNodes = tempPNode;
		}
		
	}






	public void MakeCatButton(CatagoryNode cat){
		cat.catButton = GameObject.Instantiate(genButton,new Vector3(0f,0f,0f),Quaternion.identity) as GameObject;
		cat.catButton.transform.parent = searchMenu;
		cat.catButton.AddComponent<CatButton>();
		cat.catButton.GetComponent<CatButton>().catNode = cat;
		cat.catButton.GetComponent<CatButton>().menu = this;
		cat.catButton.GetComponent<CatButton>().DrawButton();
	}

	public void KillCatButtons (){
		foreach(CatagoryNode cat in cats){
			Destroy(cat.catButton);
		}
	}

	public void MakePanButton(CatagoryNode cat){
		foreach (PanoNode p in cat.panoNodes){
		p.panButton = GameObject.Instantiate(genButton,new Vector3(0f,0f,0f),Quaternion.identity) as GameObject;
		p.panButton.transform.parent = searchMenu;
		p.panButton.AddComponent<PanButton>();
		p.panButton.GetComponent<PanButton>().panNode = p;
		p.panButton.GetComponent<PanButton>().menu = this;
		p.panButton.GetComponent<PanButton>().DrawButton();
			buttons.Add(p.panButton);
		}
	}

	public void KillPanButtons (){
		foreach(GameObject b in buttons){
			Destroy(b);
		}
		buttons.Clear();
	}


	public void SelectServerPano(string s){
		Debug.Log(s);
		StartCoroutine("DownloadPanno",s);
	}



	public void TodaysPano(){
		StartCoroutine("DownloadPanno");
	}


	public void CatSelect(CatagoryNode cat){
		KillCatButtons ();
		MakePanButton(cat);
	}


	public void PanSelect(PanoNode pan){
		//string s = new string("http://viral3d.com/" + pan.folder + "/" + pan.name) as string;
		string s = url + pan.folder + "/" + pan.name;
		Debug.Log(s);
		StartCoroutine("DownloadPanno",s);
	}


	IEnumerator DownloadPanno (string s){
		foreach(GameObject b in buttons){
			b.GetComponent<Button>().interactable = false;
		}
		loadButton.GetComponentInChildren<Text>().text = "Loading";
		loadButton.GetComponent<Button>().interactable = false;
		Debug.Log("start");
		WWW www = new WWW(s);
		yield return www;
		Debug.Log("finish");
		SetPano(www.texture);

		foreach(GameObject b in buttons){
			b.GetComponent<Button>().interactable = true;
		}
		loadButton.GetComponent<Button>().interactable = true;
		loadButton.GetComponentInChildren<Text>().text = "View Pano";
	}


	public void HuntPic(){
		EtceteraAndroid.promptForPictureFromAlbum(4096,2048,"Pano");
	}

	public void ViewPanno(){
		Application.LoadLevel(2);
	}

	public void TakePano(){
		EtceteraAndroid.promptToTakePhoto(4096,2048,"random");
	}

	// Texture loading delegates
	public void textureLoaded( string imagePath, Texture2D texture ){
		SetPano(texture);
	}

	public void SetPano(Texture2D texture){
		Pano.pic = texture;
		image.texture = Pano.pic;
	}

	public void StopSearchDownload(){
		StopCoroutine("DownloadPanno");
		foreach(GameObject b in buttons){
			b.GetComponent<Button>().interactable = true;
		}
		loadButton.GetComponentInChildren<Text>().text = "View Pano";
		KillPanButtons();
		SetUpButtons();
	}
	
}



public static class Pano {

	public static Texture2D pic;
}


[System.Serializable]
public class CatagoryNode{
	public string name;
	public List <PanoNode> panoNodes;
	public GameObject catButton;
}

[System.Serializable]
public class PanoNode{
	public string name;
	public string seoName;
	public string site;
	public string folder;
	public float latitude;
	public float longitude;
	public string discription;
	public GameObject panButton;
}




