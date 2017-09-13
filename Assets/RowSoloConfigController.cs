using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowSoloConfigController : MonoBehaviour {

//	CanvasController canvasController = null;
	public RowSessionManager rowSessionManager;
	// Use this for initialization
	void Start () {
//		canvasController = GameObject.Find ("Canvas").GetComponent<CanvasController> ();
	}
	
	public void OnOk(){
//		canvasController.Hide ();
		rowSessionManager.InitRowingSession (true);
	}
}
