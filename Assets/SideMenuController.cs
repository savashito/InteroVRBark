using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMenuController : MonoBehaviour {


	CanvasController canvasController;
	void Start(){
		canvasController = GameObject.Find ("Canvas").GetComponent<CanvasController> ();
		
	}

	public void OnHideSideMenuButton(){
		print ("OnHideSideMenuButton");
		canvasController.HideSideMenu ();
	}
}
