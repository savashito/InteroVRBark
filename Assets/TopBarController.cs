using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBarController : MonoBehaviour {

	CanvasController canvasController;

	void Start(){
		canvasController = GameObject.Find ("Canvas").GetComponent<CanvasController> ();
	}
	public void OnSideMenu(){
		canvasController.DisplaySideMenu ();
	}
	public void OnSettings(){
		canvasController.DisplaySettings ();
	}

}
