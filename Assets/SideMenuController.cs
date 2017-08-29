using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMenuController : MonoBehaviour {


	public CanvasController canvasController ;
	void Start(){
//		canvasController = GameObject.Find ("Canvas").GetComponent<CanvasController> ();
		
	}

	public void OnHideSideMenuButton(){
		print ("OnHideSideMenuButton");
//		print (canvasController);
//		if (canvasController == null) {
//			canvasController = GameObject.Find ("Canvas").GetComponent<CanvasController> ();
//			print ("found canvasController" + canvasController);
//		}
//		print (canvasController);
		canvasController.HideSideMenu ();
	}

}
