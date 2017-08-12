using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmpezarPartida : MonoBehaviour {

	private bool iniciar = false;
	public GameObject GO;

	// Use this for initialization
	void Start () {
		
	}

	private IEnumerator Contar()
	{
		
		yield return new WaitForSeconds(1.0f);
		GO.GetComponentInChildren<Text> ().text = "3";
		yield return new WaitForSeconds(1.0f);
		GO.GetComponentInChildren<Text> ().text = "2";
		yield return new WaitForSeconds(1.0f);
		GO.GetComponentInChildren<Text> ().text = "1";
		yield return new WaitForSeconds(1.0f);
		GO.GetComponentInChildren<Text> ().text = "Go!";

		this.GetComponent<PlayerNetwork> ().myGhost.GetComponent<GhostController> ().enabled = true;
		this.GetComponent<PlayerNetwork> ().myPlayer.GetComponent<Animator> ().SetBool ("iniciar", true);
		this.GetComponent<PlayerNetwork> ().myPlayer.GetComponent<PlayerController> ().iniciar = true;

		yield return new WaitForSeconds(1.0f);
		Destroy (GO.transform.parent.gameObject);
	}

	// Update is called once per frame
	void Update () {
		bool esperar=false;
		if (!iniciar) {
			foreach (GameObject jugador in GameObject.FindGameObjectsWithTag("Player")) {
				if (jugador.transform.position.x < -0.1) {
					esperar = true;
				}
			}
			if (!esperar) {
				iniciar = true;
				GO.SetActive (true);
				GO.GetComponentInChildren<Text> ().text = "";

				StartCoroutine ("Contar");
			}
		}
	}
}
