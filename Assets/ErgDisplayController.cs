using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErgDisplayController : MonoBehaviour {

	public Text mSPM;
	public Text mWatts;
	public Text mDistance;
	public Text mTime;
	public Text mPace;
	void setSPM(float t){
		mSPM.text = ""+(int)t;
	}
	void setPace(float t){
		mPace.text = "" + t;
	}
	void setTime(float t){
		mTime.text = "" + timeToString(t);
	}
	void setDistance(float m){
		mWatts.text = "" + m;
	}
	void setWatts(float w){
		mWatts.text = "" + w;
	}

	// Use this for initialization
	void Start () {
		print ("Meow");
		setSPM (32);
	}
	public static string timeToString(float t){
		float m = Mathf.Floor(t/60);
		float s = Mathf.Floor(t%60);
		string sSeg,sMin;
		if (s < 10)
			sSeg = string.Format ("0{0}",s);
		else
			sSeg = string.Format ("{0}",s);
		if (m < 1)
			sMin = "";
		else
			sMin = string.Format ("{0}",m);
		return string.Format("{0}:{1}",sMin,sSeg);
	}

	public void SetFields(ErgData e){
		float d = e.distance, t = e.time;
		float power = e.power, pace = e.pace;
		float spm=e.spm;

		mDistance.text = string.Format("{0}",Mathf.Floor(d));
		mTime.text = timeToString (t);
		mWatts.text = string.Format("{0}",power);
		mPace.text = string.Format("{0}",timeToString(pace));
		mSPM.text = string.Format("{0}",spm);
	}
}
