using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUtil : MonoBehaviour {

	public Text textDebug;
	public Text textDistance;
	public Text textParcial;
	public Text textWatts;
	public Text textTime;
	public Text textSPM;



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

		textDistance.text = string.Format("{0}",Mathf.Floor(d));
		textTime.text = timeToString (t);
		textWatts.text = string.Format("{0}",power);
		textParcial.text = string.Format("{0}",timeToString(pace));
		textSPM.text = string.Format("{0}",spm);
	}
}
