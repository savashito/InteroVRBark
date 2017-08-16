using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestionController : MonoBehaviour {

	public Toggle toggleRowCrew;
	public Toggle toggleVRHeadset;
	public Slider sliderCardio;
	public Text textSlider;

	public void OnSlide(){
		textSlider.text = ""+sliderCardio.value;
	}
	public void OnContinue(){
		print ("ToggleRowCrew is " + toggleRowCrew.isOn);
		print ("Toggle VR hedset is " + toggleVRHeadset.isOn);
		print ("Slider is " + sliderCardio.value);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
