using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class CanvasController : MonoBehaviour {

	GameObject TopBar;

	GameObject InitialSetupView;
//	GameObject SelectTeamView;
	GameObject FBLogingView;
	GameObject MainMenuView;
	GameObject CreateWOGView;

	GameObject RowTeamConfigView;
	GameObject RowRivalConfigView;
	GameObject RowSoloConfigView;

	GameObject Background;
	GameObject LoginBackground;

	GameObject Settings;
	GameObject SideMenu;

	GameObject Sync;
	GameObject Log;
	GameObject Coach;
	GameObject Leader;

	GameObject canvas;
	void Start(){
		InitialSetupView = transform.Find ("InitialSetup").gameObject;
		TopBar  = transform.Find ("TopBar").gameObject;
//		SelectTeamView = transform.Find ("SelectTeam").gameObject;
		FBLogingView = transform.Find ("FBLoging").gameObject;
		CreateWOGView = transform.Find ("CreateWOG").gameObject;
		MainMenuView = transform.Find ("MainMenu").gameObject;
		Background = transform.Find ("Background").gameObject;
		LoginBackground = transform.Find ("LoginBackground").gameObject;

		RowTeamConfigView = transform.Find ("RowTeamConfig").gameObject;
		RowRivalConfigView = transform.Find ("RowRivalConfig").gameObject;
		RowSoloConfigView = transform.Find ("RowSoloConfig").gameObject;

		Settings = transform.Find ("Settings").gameObject;
		SideMenu = transform.Find ("SideMenu").gameObject;

		Sync = transform.Find ("Sync").gameObject;
		Log = transform.Find ("Log").gameObject;
		Coach = transform.Find ("Coach").gameObject;
		Leader = transform.Find ("Leader").gameObject;
		print ("CanvasController.Start RowSoloConfigView" + RowSoloConfigView);


//		Image image = gameObject.GetComponent<Image>();
//		image.sprite = Resources.Load<Sprite> ("UIAssets\\Background1.png");
	}
	public void Hide(){
		gameObject.SetActive (false);
//		transform.SetA
	}
	public void OnLogout(){
		FB.LogOut();
		HideAllViews ();
		DisplayFBLogingView ();
	} 
	public void HideSideMenu(){
		SideMenu.SetActive (false);
	}
	void HideAllViews(){
//		SelectTeamView.SetActive (false);
		FBLogingView.SetActive (false);
		CreateWOGView.SetActive (false);
		MainMenuView.SetActive (false);
		Background.SetActive(false);
		LoginBackground.SetActive(false);
		Settings.SetActive (false);
		SideMenu.SetActive (false);
		Sync.SetActive (false);
		Log.SetActive (false);
		Leader.SetActive (false);
		Coach.SetActive (false);
	}


	public void DisplaySideMenu(){
//		HideAllViews ();
//		TopBar.SetActive(false);
		SideMenu.SetActive(true);
	}
	public void DisplaySettings(){
		SideMenu.SetActive (false);
//		HideAllViews ();
		Settings.SetActive(true);
//		TopBar.SetActive (false);
	}

	public void DisplaySync(){
		HideAllViews ();
		Sync.SetActive(true);
	}
	public void DisplayCoach(){
		HideAllViews ();
		Coach.SetActive(true);
	}
	public void DisplayLog(){
		HideAllViews ();
		Log.SetActive(true);
	}
	public void DisplayLeader(){
		HideAllViews ();
		Leader.SetActive(true);
	}
	/*
	public void HideSettings(){
		Settings.SetActive(false);
		TopBar.SetActive (true);
	}*/
	/*
	public void DisplaySelectTeamView(){
		HideAllViews ();
//		Background.SetActive(true);
		SelectTeamView.SetActive (true);
	}*/
	public void DisplayRowSoloConfig(){
		HideAllViews ();
//		Background.SetActive(true);
		print ("DisplayRowSoloConfig" + RowSoloConfigView);
		RowSoloConfigView.SetActive (true);
	}
	public void DisplayRowRivalConfig(){
		HideAllViews ();
//		Background.SetActive(true);
		RowRivalConfigView.SetActive (true);
	}
	public void DisplayRowTeamConfig(){
		HideAllViews ();
//		Background.SetActive(true);
		RowTeamConfigView.SetActive (true);
	}
	public void DisplayFBLogingView(){
		HideAllViews ();
		LoginBackground.SetActive(true);
		FBLogingView.SetActive (true);
	}
	public void DisplayCreateWOGView(){
		HideAllViews ();
//		Background.SetActive(true);
		CreateWOGView.SetActive (true);
	}
	public void DisplayInitialSetup(){
		InitialSetupView.SetActive(true);
	}
	public void DisplayMainMenuView(){
		HideAllViews ();
//		Background.SetActive(true);
		InitialSetupView.SetActive(false);
		TopBar.SetActive (true);
		MainMenuView.SetActive (true);
	}
	public void SetRower(JSONObject jRower){

	}
}
