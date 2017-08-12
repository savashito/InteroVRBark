using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Puntero : MonoBehaviour {

	private Button botonStart;
	private UnityEngine.EventSystems.EventSystem myEventSystem;
	private float tiempo;
	public Image Carga;
	// Use this for initialization
	void Start () {
		myEventSystem=GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 fwd = Camera.main.transform.forward;
		RaycastHit hit;

		if (Physics.Raycast (transform.position, fwd, out hit, 100)) {
			if (hit.collider.gameObject.name == "Start") {
				botonStart = hit.collider.gameObject.GetComponent<Button> ();
				myEventSystem.SetSelectedGameObject(botonStart.gameObject);
				if (tiempo+3.0f < Time.time) {
					Destroy (botonStart.gameObject);
					botonStart = null;
					Transform myRower = transform.root;
					myRower.position = new Vector3 (0.0f, myRower.position.y, myRower.position.z); 
					Destroy (this.gameObject);
				}
				Carga.fillAmount = (Time.time - tiempo) / 3.0f;
			}
		} else {
			myEventSystem.SetSelectedGameObject(null);
			tiempo = Time.time;
		}
	}
}
