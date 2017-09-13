using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.UI;

public class VRController : MonoBehaviour {
//	public Canvas canvas;
	public CanvasController canvasController;
	public RowSessionManager rowSessionManager;
	public InteroServerConnection interoServerConnection;
	void Start(){
		//		LoadVR ();
		// Set Portrait
		Screen.orientation = ScreenOrientation.Portrait;
		// add callbacks to buttons, sliders, etc
		GameObject canvas = GameObject.Find("Canvas");

		Button[] btns = canvas.GetComponentsInChildren<Button> (true);
		Slider[] sliders = canvas.GetComponentsInChildren<Slider> (true);
		Dropdown[] dropdowns = canvas.GetComponentsInChildren<Dropdown> (true);
		print ("Button Start events");
		/*
		foreach (Slider s in sliders) {
			slider
		}*/
		/*
		foreach (Button button in btns) {
			string s = "";
			Text t = button.GetComponentInChildren<Text> ();
			if (t != null) {
//				print (button.gameObject.name);
//				print (t.text);
				s = t.text;
			}
			button.onClick.AddListener(delegate() { interoServerConnection.SendButtonClick(button.gameObject.name,s); });
		}*/
		print ("Button End events");
	}
	/*
	void  OnApplicationFocus(bool hasFocus){
		print ("OnApplicationFocus()");
		interoServerConnection.Send ("OnApplicationFocus", ""+hasFocus);
	}
	void  OnApplicationPause(bool pause){
		print ("OnApplicationPause()");
		interoServerConnection.Send ("OnApplicationPause", ""+pause);
	}
	void  OnApplicationQuit(){
		print ("OnApplicationQuit()");
		interoServerConnection.Send ("OnApplicationQuit", "");
	}
	*/
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			// Android close icon or back button tapped.
			// Application.Quit();
			VROFF();
			canvasController.Show ();
			// send event exit
			rowSessionManager.EndRowingSession ();

		}
	}
	void OnMouseDown ()
	{
		print ("OnMOuseDown");
	}
	public void Recenter(){
		GvrCardboardHelpers.Recenter();
		//		print ("Recenter " +  iFrame);
		//		iFrame = 0;
		//		GvrViewer.Instance.Recenter ();

		//		transform.localRotation = Quaternion.Inverse(VRcam.rotation);
	}
	/*

	public void LoadVR(){
		Screen.orientation = ScreenOrientation.Landscape;
		VRSettings.LoadDeviceByName("cardboard");
	}
	public void UnloadVR(){
		VRSettings.LoadDeviceByName("");
	}
	public void SetVR(bool mode){
		// set the screen for wide rendering

		if (mode) {
			Screen.orientation = ScreenOrientation.Landscape;
			// SwitchToVR ();
			VRSettings.enabled = true;
		} else {
//			SwitchOutOfVr ();

			VRSettings.enabled = false;
			Screen.orientation = ScreenOrientation.Portrait;
			ResetCameras();
		}
	}*/

	public void VRON(){
		//		canvas.gameObject.SetActive (false);
		StartCoroutine (SwitchToVR ());
	}
	public void VROFF(){
		StartCoroutine (SwitchOutOfVr ());
		//		canvas.gameObject.SetActive (true);

		//		StartCoroutine (SwitchOutOfVr ());
	}
	IEnumerator SwitchToVR() {
		Screen.orientation = ScreenOrientation.Landscape;
		VRSettings.LoadDeviceByName("cardboard"); // Or "cardboard" (both lowercase).

		// Wait one frame!
		yield return null;

		// Now it's ok to enable VR mode.
		VRSettings.enabled = true;
		//		Screen.orientation = ScreenOrientation.AutoRotation;
	}
	IEnumerator SwitchOutOfVr() {
		//		Screen.orientation = ScreenOrientation.AutoRotation;
		VRSettings.LoadDeviceByName(""); // Empty string loads the "None" device.
		Screen.orientation = ScreenOrientation.AutoRotation;
		// Wait one frame!
		yield return null;

		// Not needed, loading the None (`""`) device automatically sets `VRSettings.enabled` to `false`.
		VRSettings.enabled = false;

		// If you only have one camera in your scene, you can just call `Camera.main.ResetAspect()` instead.
		ResetCameras();
		Screen.orientation = ScreenOrientation.AutoRotation;
	}

	// Calls `ResetAspect()` on all enabled VR capable cameras to restore their non-VR configuration.
	void ResetCameras() {
		// Camera looping logic copied from GvrEditorEmulator.cs
		for (int i = 0; i < Camera.allCameras.Length; i++) {
			Camera cam = Camera.allCameras[i];
			if (cam.enabled && cam.stereoTargetEye != StereoTargetEyeMask.None) {
				// Reset aspect ratio based on normal (non-VR) screen size.
				cam.ResetAspect();
				// Don't need to reset camera `fieldOfView`, since it's restored to the original value automatically.
			}
		}
	}
}
