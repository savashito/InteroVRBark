using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBarController : MonoBehaviour {


	GameObject SettingsButton;
	GameObject SideMenuButton;
	//	GameObject CloseSettingsButton;
	//	GameObject CloseMenuButton;

	CanvasController canvasController;
	void Start(){
		canvasController = GameObject.Find ("Canvas").GetComponent<CanvasController> ();
		SettingsButton = transform.Find ("SettingsButton").gameObject;
//		CloseSettingsButton = transform.Find ("CloseSettingsButton").gameObject;
		SideMenuButton = transform.Find ("SideMenuButton").gameObject;
//		CloseMenuButton = transform.Find ("SideMenuAcceptButton").gameObject;
	}
	public void OnSideMenu(){
		canvasController.DisplaySideMenu ();
//		SettingsButton.SetActive (false);
//		SideMenuButton.SetActive (false);
//		CloseMenuButton.SetActive (true);
//		CloseSettingsButton.SetActive (false);
	}
	public void OnSettings(){
		canvasController.DisplaySettings ();
//		SettingsButton.SetActive (false);
//		SideMenuButton.SetActive (false);
//		CloseMenuButton.SetActive (false);
//		CloseSettingsButton.SetActive (true);
	}
//	public void OnAcceptSettings(){
//		HideSettings ();
//	}
	/*
	public void OnCancelSettings(){
		HideSettings ();
	}

	public void OnCancelSettings(){
		HideSettings ();
	}*/
//	public void HideSettings(){
//		canvasController.HideSettings ();
//		SettingsButton.SetActive (true);
//		SideMenuButton.SetActive (true);
//		CloseMenuButton.SetActive (false);
//		CloseSettingsButton.SetActive (false);
//	}
//	public void HideSideMenu(){
//		canvasController.HideSideMenu ();
//		SettingsButton.SetActive (true);
//		SideMenuButton.SetActive (true);
//		CloseMenuButton.SetActive (false);
//		CloseSettingsButton.SetActive (false);
//	}
}
