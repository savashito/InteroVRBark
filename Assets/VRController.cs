using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class VRController : MonoBehaviour {
//	public Canvas canvas;
	public CanvasController canvasController;
	public RowSessionManager rowSessionManager;
	void Start(){
		//		LoadVR ();
		// Set Portrait
		Screen.orientation = ScreenOrientation.Portrait;
	}
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			// Android close icon or back button tapped.
			// Application.Quit();
			VROFF();
			canvasController.Show ();
			rowSessionManager.EndRowingSession ();

		}
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
