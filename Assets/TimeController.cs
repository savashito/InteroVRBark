using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeController : MonoBehaviour {
	Dropdown dropdownTime;
	int [] listTimes = new int []{ 15, 30, 60, 120, 60*5, 600, 60*15, 60*30 };
	int [] listTimesBase = new int []{ 15, 30, 60, 120, 60*5, 600, 60*15, 60*30 };
	// List<string> listStringTimes = new List<string>(new string[] 
	// 		{ "15 s", "30 s", "1 m", "2 m", "5 m", "10 m", "15 m", "30 m" });
	void Start(){
		dropdownTime = transform.Find ("DropdownTime").gameObject.GetComponent<Dropdown>();
		print("TimeController.Start "+dropdownTime);
		dropdownTime.value = 0;
		UpdateOptions();

	}
	void UpdateListTimes(int timeOffset){
		for(int i=0;i<listTimes.Length;++i){
			listTimes[i]=listTimesBase[i]+timeOffset;
		}
	}
	public int GetTimeIndex(){
		return listTimes[dropdownTime.value];
	}
	public string GetTimeString(){
		return SecondsToStringTime(listTimes[dropdownTime.value]);//+" s";
	}
	public void UpdateOptions(TimeController parentSelectedTimeController){
		UpdateListTimes(parentSelectedTimeController.GetTimeIndex());
		UpdateOptions();
	}
	public void UpdateOptions(){
		dropdownTime.ClearOptions();		
		for(int i=0;i<listTimes.Length;++i){
			dropdownTime.options.Add(new Dropdown.OptionData(""+SecondsToStringTime(listTimes[i])));
		}
		
		// dropdownTime.options.Add (new UnityEngine.UI.Dropdown.OptionData () { text = "yes" });
		// dropdownTime.AddOptions(new Dropdown.OptionData("Jojo"));
	}
	// private functions
	string SecondsToStringTime(int seconds){
		if(seconds<60){
			return seconds+" s";
		}else{
			return seconds/60+" m";
			// return string.Format("{0:f1} m",seconds/60.0f);
		}
	}
}

