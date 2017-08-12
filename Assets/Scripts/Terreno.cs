using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terreno : MonoBehaviour {

	private GameObject jugador;
	public GameObject[] objeto;
	public int cantidadObjetos=100;
	public string etiqueta;

	// Use this for initialization
	void Start () {
		float HM = Random.Range(5, 10);
		float divRange = Random.Range(6,15);
		GenerateTerrain(this.GetComponent<Terrain>(), HM, divRange);
		jugador = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update(){

		if (jugador == null) {
			jugador = GameObject.FindGameObjectWithTag ("Player");
			return;
		}

		if (jugador.transform.position.x > this.transform.position.x + 375) {
			this.transform.position = new Vector3 (this.transform.position.x + 500, this.transform.position.y, this.transform.position.z);
			float HM = Random.Range(5, 10);
			float divRange = Random.Range(6,15);
			foreach (GameObject objeto in GameObject.FindGameObjectsWithTag(etiqueta)) {
				GameObject.Destroy (objeto);
			}
			GenerateTerrain(this.GetComponent<Terrain>(), HM, divRange);
		}
	}
	
	public void GenerateTerrain(Terrain t, float HM, float divRange)
	{

		 //Generate new Heights for terreain
		//Heights For Our Hills/Mountains
		float[,] hts = new float[t.terrainData.heightmapWidth, t.terrainData.heightmapHeight];
		float temp1=divRange;
		float temp2=divRange;
		float x = 0;
		float y = Mathf.PI/2;
		for (int i = 0; i < t.terrainData.heightmapWidth; i++)
		{
			for (int k = 0; k < t.terrainData.heightmapHeight; k++)
			{
				temp2 = 50*Mathf.Sin(y)+divRange;
				if (temp2 < 0) {
					temp2 = 0;
				}
				y += (Mathf.PI*2) / t.terrainData.heightmapHeight;

				if (i > 110 && i < 156) {
					hts [i, k] = 0;
				} else {
					hts [i, k] = Mathf.PerlinNoise (((float)i / (float)t.terrainData.heightmapWidth) * HM, ((float)k / (float)t.terrainData.heightmapHeight) * HM) / ((temp1 + temp2) / 2);
				}
			}
			y = Mathf.PI/2;
			temp2=divRange;
			temp1 = 50*Mathf.Sin(x)+divRange;
			x += Mathf.PI / t.terrainData.heightmapWidth;
		}
		//Debug.LogWarning("DivRange: " + divRange + " , " + "HTiling: " + HM);
		t.terrainData.SetHeights(0, 0, hts);
		int mz;
		int nx;
		float altura;
		for(int j=0; j<100; j++){
			mz = Random.Range(-250, 250);
			nx = Random.Range ((int)this.transform.position.x, (int)this.transform.position.x+250);
			if (mz > -21 && mz < 56) {
				j--;
			}else{
				altura = t.SampleHeight (new Vector3 (nx, 0, mz));
				GameObject arbol = Instantiate(objeto[Random.Range(0, objeto.Length)], new Vector3(nx, altura-10, mz), Quaternion.Euler(new Vector3(0, 180, 0)));
				arbol.tag = etiqueta;
			}
		}
	}
}