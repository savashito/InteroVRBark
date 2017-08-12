using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform VRcam;  // drag the child VR cam here in the inspector
	public int nFrames = 80;
	private int iFrame;
	private float prevRotation;
	public Camera fpv;
	public Camera tpv;

	public void Recenter(){
//		print ("Recenter " +  iFrame);
		iFrame = 0;
//		GvrViewer.Instance.Recenter ();

//		transform.localRotation = Quaternion.Inverse(VRcam.rotation);
	}
	// Use this for initialization
	void Start () {
		iFrame = 0;

//		fpv = GameObject.Find("1PCamera").GetComponent<Camera>();
//		tpv = GameObject.Find("3PCamera").GetComponent<Camera>();

		prevRotation = VRcam.eulerAngles.y;
		// Invoke("LateStart", 0.1f);
//		fpv = transform.Find("1PCamera").gameObject.GetComponent<Camera>();
//		tpv = transform.Find("3PCamera").gameObject.GetComponent<Camera>();
//		print ("FPV called");
//	}
//
//	void LateStart(){
////		aFinger = transform.Find("LeftShoulder/Arm/Hand/Finger");
//		fpv = transform.Find("1PCamera").gameObject.GetComponent<Camera>();
//		tpv = transform.Find("3PCamera").gameObject.GetComponent<Camera>();
////		print ("lateStart " + f);
////		fpv = GameObject.Find("1PCamera").GetComponent<Camera>();
////		tpv = GameObject.Find("3PCamera").GetComponent<Camera>();
//	}
//	public void setFPV (){
////		print ("setFPV");
//		fpv = transform.Find("1PCamera").gameObject.GetComponent<Camera>();
//		tpv = transform.Find("3PCamera").gameObject.GetComponent<Camera>();
////
//		fpv.enabled = true;
//		tpv.enabled = false;
////
//	}
//	public void setTPV(){
//		fpv = transform.Find("1PCamera").gameObject.GetComponent<Camera>();
//		tpv = transform.Find("3PCamera").gameObject.GetComponent<Camera>();
//		fpv.enabled = false;
//		tpv.enabled = true;
	}
	// Update is called once per frame
	void Bark () {
		// reset center if user has not moved camera more than 5 degrees for over 3 sec.
//		80 frames

		if (Mathf.Abs (prevRotation - VRcam.eulerAngles.y) < 2.0f) {
//			print ("Recenter "+ iFrame);
			iFrame++;
//			GvrViewer.Instance.Recenter ();
		} else {

			iFrame = 0;
		}
		if (iFrame > 2) {
			Recenter ();
		}
//		print ("called fix update");
		prevRotation = VRcam.eulerAngles.y;
		//print (VRcam.eulerAngles.y>30);
		/*
		if (Input.GetKeyDown ("space")) {
			print ("space key was pressed");
//			Recenter ();
			GvrViewer.Instance.Recenter ();

		}*/

	}
}

