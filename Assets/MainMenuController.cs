using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {
	public InteroServerConnection interoServer;
	public CanvasController canvasController = null;
	// Use this for initialization
	void Start () {
		canvasController = GameObject.Find ("Canvas").GetComponent<CanvasController> ();
	}
	public void GroupRow(){
		// check to see if there are any people of my group rowing, if so take me rowing with them.
//		interoServer
		// otherwise, take me to choose enviroment screen
		interoServer.GetTeamWorkouts();
	}

	public void RivalRow(){
		// check to see if there are any people rowing in general and make me row with them
		interoServer.GetRivalWorkouts();
		// otherwise, take me to choose enviroment screen
	}

	public void SoloRow(){
		// just make me row by my own
		canvasController.Hide ();
	}
}
