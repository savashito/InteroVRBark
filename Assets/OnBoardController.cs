using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoardController : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		print ("OnBoardController.Start");
		PM5EventHandler.requestBLEAccess ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
