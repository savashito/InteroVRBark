using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideMenuController : MonoBehaviour {


	public CanvasController canvasController ;
	public Text name;
	public Text team;
	public Text pts;
	void Start(){
//		canvasController = GameObject.Find ("Canvas").GetComponent<CanvasController> ();
//		name = transform.Find("Name").GetComponent<Text>();//gameObject.GetComponentInChildren<>
//		team = transform.Find("Team").GetComponent<Text>();//gameObject.GetComponentInChildren<>
//		pts = transform.Find("Pts").GetComponent<Text>();//gameObject.GetComponentInChildren<>
	
	}
	public void UpdateMenuHUD(string namei,string teami,string ptsi){
		print ("UpdateMenuHUD " + namei + " " + teami);
		name.text = namei;
		team.text = teami;
		pts.text = ptsi;
		print ("UpdateMenuHUDEnd " + namei + " " + teami);
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
	public void OnLearnHowToRow(){
		Application.OpenURL ("https://www.youtube.com/watch?v=zQ82RYIFLN8");
	}

}
