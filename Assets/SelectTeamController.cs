using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTeamController : MonoBehaviour {
	public InteroServerConnection interoServer;

	public void JoinRed(){
		interoServer.JoinTeam ("red");
	}
	public void JoinGreen(){
		interoServer.JoinTeam ("green");
	}
	public void JoinBlue(){
		interoServer.JoinTeam ("blue");
	}

}
