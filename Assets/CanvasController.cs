using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

	GameObject SelectTeamView;
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

	GameObject canvas;
	void Start(){
		SelectTeamView = transform.Find ("SelectTeam").gameObject;
		FBLogingView = transform.Find ("FBLoging").gameObject;
		CreateWOGView = transform.Find ("CreateWOG").gameObject;
		MainMenuView = transform.Find ("MainMenu").gameObject;
		Background = transform.Find ("Background").gameObject;
		LoginBackground = transform.Find ("LoginBackground").gameObject;

		RowTeamConfigView = transform.Find ("RowTeamConfig").gameObject;
		RowRivalConfigView = transform.Find ("RowRivalConfig").gameObject;
		RowSoloConfigView = transform.Find ("RowSoloConfig").gameObject;

		Settings= transform.Find ("Settings").gameObject;
		SideMenu= transform.Find ("SideMenu").gameObject;
		print ("CanvasController.Start RowSoloConfigView" + RowSoloConfigView);


//		Image image = gameObject.GetComponent<Image>();
//		image.sprite = Resources.Load<Sprite> ("UIAssets\\Background1.png");
	}
	public void Hide(){
		gameObject.SetActive (false);
//		transform.SetA
	}
	void HideAllViews(){
		SelectTeamView.SetActive (false);
		FBLogingView.SetActive (false);
		CreateWOGView.SetActive (false);
		MainMenuView.SetActive (false);
		Background.SetActive(false);
		LoginBackground.SetActive(false);
	}


	public void DisplaySideMenu(){
//		HideAllViews ();
		SideMenu.SetActive(true);
	}
	public void DisplaySettings(){
//		HideAllViews ();
		Settings.SetActive(true);

	}
	public void DisplaySelectTeamView(){
		HideAllViews ();
		Background.SetActive(true);
		SelectTeamView.SetActive (true);
	}
	public void DisplayRowSoloConfig(){
		HideAllViews ();
		Background.SetActive(true);
		print ("DisplayRowSoloConfig" + RowSoloConfigView);
		RowSoloConfigView.SetActive (true);
	}
	public void DisplayRowRivalConfig(){
		HideAllViews ();
		Background.SetActive(true);
		RowRivalConfigView.SetActive (true);
	}
	public void DisplayRowTeamConfig(){
		HideAllViews ();
		Background.SetActive(true);
		RowTeamConfigView.SetActive (true);
	}
	public void DisplayFBLogingView(){
		HideAllViews ();
		LoginBackground.SetActive(true);
		FBLogingView.SetActive (true);
	}
	public void DisplayCreateWOGView(){
		HideAllViews ();
		Background.SetActive(true);
		CreateWOGView.SetActive (true);
	}
	public void DisplayMainMenuView(){
		HideAllViews ();
		Background.SetActive(true);
		// Check if its the first time logging in.

		MainMenuView.SetActive (true);
	}
	public void SetRower(JSONObject jRower){

	}
}
