using UnityEngine;
using System.Collections;

public class RegCamControl : MonoBehaviour {

	public float leftRightSpeed = 1f;
	public float upDownSpeed = 1f;

	public float v;
	public float h;
	

	// Update is called once per frame
	void LateUpdate () {
		v = Input.acceleration.y;
		h = Input.acceleration.x;

		//transform.Rotate(new Vector3(v*upDownSpeed,h*leftRightSpeed,0f) * Time.deltaTime);
		var rot = transform.rotation;
		//transform.rotation.eulerAngles = new Vector3(transform.rotation.eulerAngles.x + v, transform.rotation.eulerAngles.y + h, 0f);
		rot.eulerAngles = new Vector3(transform.rotation.eulerAngles.x + v * Time.deltaTime * upDownSpeed, transform.rotation.eulerAngles.y + h * Time.deltaTime * leftRightSpeed, 0f);
		//rot.eulerAngles = new Vector3((Mathf.Clamp(rot.eulerAngles.x,-60f,60f)),rot.eulerAngles.y,rot.eulerAngles.z);
		transform.rotation = rot;
	}
}
