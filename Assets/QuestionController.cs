using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestionController : MonoBehaviour {
	public InteroServerConnection interoServer;
	CanvasController canvasController;
	public Toggle toggleRowCrew;
	public Toggle toggleVRHeadset;
	public Slider sliderCardio;
	public Text textSlider;
	public ConfigurationHUD confHUD;

	void Start (){
		canvasController = GameObject.Find ("Canvas").GetComponent<CanvasController> ();
	}
	public void OnSlide(){
		textSlider.text = ""+sliderCardio.value;
	}
	public void OnContinue(){
		print ("ToggleRowCrew is " + toggleRowCrew.isOn);
		print ("Toggle VR hedset is " + toggleVRHeadset.isOn);
		print ("Slider is " + sliderCardio.value);
		interoServer.SaveQuestions (toggleRowCrew.isOn,toggleVRHeadset.isOn,sliderCardio.value);
//		canvasController.questionsAnswered(toggleRowCrew.isOn,toggleVRHeadset.isOn,sliderCardio.value);
		// set defeault configuration for user
		confHUD.setVR(toggleVRHeadset.isOn);
		confHUD.setRowBackwards(!toggleRowCrew.isOn);
	}


}
