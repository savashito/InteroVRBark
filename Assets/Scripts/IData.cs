using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IData
{
	/*
	string eventName {
		get;
	}*/
	string getEvent ();
	JSONObject ToJSON();
}

