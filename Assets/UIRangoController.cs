using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIRangoController : MonoBehaviour {
	GameObject bark;
	public GameObject next = null;
	InputField mInputMeters = null,mSPM;
	Text mStartLabel = null;
	Text textUnits;
	// Use this for initialization
	void Start () {
		if (next != null) {
			GameObject startLabel = next.transform.Find ("StartLabel").gameObject;
			mStartLabel = startLabel.GetComponent<Text> ();
		}
		GameObject endRange = transform.Find ("endRange").gameObject;
		mInputMeters = endRange.GetComponent<InputField>();
//		mInputMeters = "bark";
		GameObject spm = transform.Find ("spm").gameObject;
		mSPM = spm.GetComponent<InputField>();
		GameObject unitLabel = transform.Find ("UnitLabel").gameObject;
		textUnits = unitLabel.GetComponent<Text> ();
		print ("UnitLbael Initieed");
	}
	public void setTime(){
		GameObject unitLabel = transform.Find ("UnitLabel").gameObject;
		textUnits = unitLabel.GetComponent<Text> ();
		textUnits.text = "min";
	}
	public void setDistance(){
		GameObject unitLabel = transform.Find ("UnitLabel").gameObject;
		textUnits = unitLabel.GetComponent<Text> ();
		textUnits.text = "[m]";
	}	// Update is called once per frame

	public void setRange(float end,float spm){
		Start ();
		mInputMeters.text = end+"";
		mSPM.text = spm+"";
		updateEndRange (mInputMeters.text);
	}

	void Update () {
		
	}

	public void updateEndRange(string text){
		
//		inp.text
//		print (mInputMeters.text);
		if(mStartLabel!=null)
			mStartLabel.text = mInputMeters.text + " - ";
	}
//	public float readStartRange(){
//		return  float.Parse (mStartLabel.text);
//	}
	public float readEndRange(){
		try{
			return  float.Parse (mInputMeters.text);
		}catch(FormatException e){
			return 0.0f;
		}

	}
	public float readRangeSPM(){
		try{
			return  float.Parse (mSPM.text);
		}catch(FormatException e){
			return 0.0f;
		}
	}
}
