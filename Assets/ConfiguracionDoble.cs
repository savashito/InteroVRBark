using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ConfiguracionDoble : MonoBehaviour {

//	public Text textUnits1,textUnits2,textUnits3;
	public UIRangoController rango1, rango2, rango3;
	RangoSPM[] ranges = new RangoSPM[3];
	public Text textTitle;
	public bool isConstraintDistance;

	public void setTime(){
		isConstraintDistance = false;
		rango1.setTime ();
		rango2.setTime ();
		rango3.setTime ();
//		textUnits1.text = "[min]";
//		textUnits2.text = "[min]";
//		textUnits3.text = "[min]";
	}
	public void setDistance(){
		isConstraintDistance = true;
		rango1.setDistance ();
		rango2.setDistance ();
		rango3.setDistance ();
//		textUnits1.text = "[m]";
//		textUnits2.text = "[m]";
//		textUnits3.text = "[m]";
	}
	public void set2000Test(float time){
		setTime ();
		float parcial = time / 4.0f;
		parcial = parcial * 100.0f/90.0f;

		print (parcial);
		int min = (int)parcial / 60;
		int seg = (int)parcial - min * 60;
		print (min);
		print (seg);
		print (parcial);
		print (time);
		string sSeg = seg < 10 ? "0" + seg : seg + "";
		textTitle.text = "Pacial Objetivo " + min + ":" + sSeg;
		rango1.setRange (7.0f,20f);
		rango2.setRange (14.0f,24f);
		rango3.setRange (21.0f,28f);
	}
	public RangoSPM[] getRangeArray(){


		ranges [0] = new RangoSPM (0,rango1.readEndRange(),rango1.readRangeSPM());
		ranges [1] = new RangoSPM (rango1.readEndRange(),rango2.readEndRange(),rango2.readRangeSPM());
		ranges [2] = new RangoSPM (rango2.readEndRange(),rango3.readEndRange(),rango3.readRangeSPM());
		return ranges;
	}
	public float getSPMFrom (ErgData ergData){
		if (isConstraintDistance)
			return getSPMFromConstraint (ergData.distance);
		else {
			print ("time constr");
			return getSPMFromConstraint (ergData.time/60.0f);

		}
	}
	public float getSPMFromConstraint( float distance){
		print (ranges.Length);
		print (distance);
		for (int i =0; i<ranges.Length;++i){
			// inside range, return SPM
//			print (ranges[i].startRange + " - "+ranges[i].endRange);
			if (ranges[i]!=null && distance > ranges[i].startRange && distance <ranges[i].endRange) {
//				print ("returning range " + ranges [i].spm);
				return ranges[i].spm;
			}
		}
		// default 
		return 20.0f;
	}
}

public class RangoSPM{
	public float startRange;
	public float endRange;
	public float spm;
	public RangoSPM(float startRange, float endRange, float spm){
		this.startRange = startRange;
		this.endRange = endRange;
		this.spm = spm;
	}
	public string ToString ()
	{
		return startRange + " " + endRange + " " + spm;
	}

}
