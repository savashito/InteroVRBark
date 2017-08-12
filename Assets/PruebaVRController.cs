using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PruebaVRController : MonoBehaviour {
	public ConfiguracionDoble confDouble;
	InputField mInputMinutes = null,mInputSeconds=null,mName;
	public	SocketConnection socketConnection;
	// Use this for initialization
	void Start () {
		
		GameObject minutes = transform.Find ("min").gameObject;
		mInputMinutes = minutes.GetComponent<InputField>();
		GameObject seconds = transform.Find ("seg").gameObject;
		mInputSeconds = seconds.GetComponent<InputField>();
		GameObject nameGameObj = transform.Find ("InputName").gameObject;
		mName = nameGameObj.GetComponent<InputField>();

	}
	// Update is called once per frame
	void Update () {
		
	}
	public void Ok(){
		//read the thingis
		float min = float.Parse(mInputMinutes.text);
		float seg = float.Parse(mInputSeconds.text);
		string name;
		name = mName.text;
		socketConnection.EmitName (name);
//		print (name);
		confDouble.set2000Test (min*60.0f+seg);
	}
}
