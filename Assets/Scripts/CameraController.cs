using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.VR;

public class CameraController : MonoBehaviour {

//	public Transform VRcam;  // drag the child VR cam here in the inspector
//	public int nFrames = 80;
//	private int iFrame;
//	private float prevRotation;
//	public Camera fpv;
//	public Camera tpv;
//	void Start(){
//		LoadVR ();
//	}
//	public void Recenter(){
//		GvrCardboardHelpers.Recenter();
////		print ("Recenter " +  iFrame);
////		iFrame = 0;
////		GvrViewer.Instance.Recenter ();
//
////		transform.localRotation = Quaternion.Inverse(VRcam.rotation);
//	}
//	public void LoadVR(){
//		VRSettings.LoadDeviceByName("cardboard");
//	}
//	public void UnloadVR(){
//		VRSettings.LoadDeviceByName("");
//	}
//	public void SetVR(bool mode){
//		if (mode) {
//			VRSettings.enabled = true;
//		} else {
//			VRSettings.enabled = false;
//			ResetCameras();
//		}
//	}
//	IEnumerator SwitchOutOfVr() {
//		VRSettings.LoadDeviceByName(""); // Empty string loads the "None" device.
//
//		// Wait one frame!
//		yield return null;
//
//		// Not needed, loading the None (`""`) device automatically sets `VRSettings.enabled` to `false`.
//		// VRSettings.enabled = false;
//
//		// If you only have one camera in your scene, you can just call `Camera.main.ResetAspect()` instead.
//		ResetCameras();
//	}
//
//	// Calls `ResetAspect()` on all enabled VR capable cameras to restore their non-VR configuration.
//	void ResetCameras() {
//		// Camera looping logic copied from GvrEditorEmulator.cs
//		for (int i = 0; i < Camera.allCameras.Length; i++) {
//			Camera cam = Camera.allCameras[i];
//			if (cam.enabled && cam.stereoTargetEye != StereoTargetEyeMask.None) {
//				// Reset aspect ratio based on normal (non-VR) screen size.
//				cam.ResetAspect();
//				// Don't need to reset camera `fieldOfView`, since it's restored to the original value automatically.
//			}
//		}
//	}
//	IEnumerator SwitchToVR() {
//		 // Or "cardboard" (both lowercase).
//
//		// Wait one frame!
//		yield return null;
//
//		// Now it's ok to enable VR mode.
//		VRSettings.enabled = true;
//	}
}
	// Use this for initialization
//	void Start () {
////		iFrame = 0;
//
////		fpv = GameObject.Find("1PCamera").GetComponent<Camera>();
////		tpv = GameObject.Find("3PCamera").GetComponent<Camera>();
//
//		prevRotation = VRcam.eulerAngles.y;
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
//	}

	// Update is called once per frame
//	void Bark () {
//		// reset center if user has not moved camera more than 5 degrees for over 3 sec.
////		80 frames
//
//		if (Mathf.Abs (prevRotation - VRcam.eulerAngles.y) < 2.0f) {
////			print ("Recenter "+ iFrame);
//			iFrame++;
////			GvrViewer.Instance.Recenter ();
//		} else {
//
//			iFrame = 0;
//		}
//		if (iFrame > 2) {
//			Recenter ();
//		}
////		print ("called fix update");
//		prevRotation = VRcam.eulerAngles.y;
//		//print (VRcam.eulerAngles.y>30);
//		/*
//		if (Input.GetKeyDown ("space")) {
//			print ("space key was pressed");
////			Recenter ();
//			GvrViewer.Instance.Recenter ();
//
//		}*/
//
//	}
//}

