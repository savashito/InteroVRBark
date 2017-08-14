using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

	GameObject SelectTeamView;
	GameObject FBLogingView;
	GameObject MainMenuView;
	GameObject CreateWOGView;
	GameObject canvas;
	void Start(){
		SelectTeamView = transform.Find ("SelectTeam").gameObject;
		FBLogingView = transform.Find ("FBLoging").gameObject;
		CreateWOGView = transform.Find ("CreateWOG").gameObject;
		MainMenuView = transform.Find ("MainMenu").gameObject;
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
	}
	public void DisplaySelectTeamView(){
		HideAllViews ();
		SelectTeamView.SetActive (true);
	}
	public void DisplayFBLogingView(){
		HideAllViews ();
		FBLogingView.SetActive (true);
	}
	public void DisplayCreateWOGView(){
		HideAllViews ();
		CreateWOGView.SetActive (true);
	}
	public void DisplayMainMenuView(){
		HideAllViews ();
		// Check if its the first time logging in.

		MainMenuView.SetActive (true);
	}
	public void SetRower(JSONObject jRower){

	}
}
