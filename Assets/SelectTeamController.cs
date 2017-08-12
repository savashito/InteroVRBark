using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTeamController : MonoBehaviour {
	public InteroServerConnection interoServer;
	CanvasController canvasController;
	// Use this for initialization
	void Start(){
		canvasController = GameObject.Find ("Canvas").GetComponent<CanvasController> ();
	}
	public void JoinRed(){
		interoServer.JoinTeam ("red");
		canvasController.DisplayMainMenuView ();
	}
	public void JoinGreen(){
		interoServer.JoinTeam ("green");
		canvasController.DisplayMainMenuView ();
	}
	public void JoinBlue(){
		interoServer.JoinTeam ("blue");
		canvasController.DisplayMainMenuView ();
	}

}
