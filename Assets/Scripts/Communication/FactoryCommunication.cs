using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Depending on the runtime enviroment constructs the object ( I speek joda Like)
public class FactoryCommunication : MonoBehaviour {
	public SocketConnection socketConnection;
	public BLEConnection bleConnection;
	private ErgDataAbstract ergDataAbstract;
	// Use this for initialization
	public void Init (ErgDataAbstract ergDataAbstract) {
	/*
		#if UNITY_STANDALONE || UNITY_EDITOR
		socketConnection.Init(ergDataAbstract);
		#else
		//bleConnection.Init(ergDataAbstract);
		#endif
		*/
//		socketConnection.Init(ergDataAbstract);
	}
	public void Start(){
		socketConnection.InitSocket ();
	}
	/*
	virtual public void OnErgData (ErgData ergData){
//		print (ergData.ToString());
		ergDataAbstract.OnErgData(ergData);
	}*/
}
