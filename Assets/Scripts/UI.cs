using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public GameObject heros;
	public Text rubisDisplay;
	public Text coeurDisplay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// si le héros n'est pas mort
		if (heros != null) {
			Ramasseur ramasseur = heros.GetComponent("Ramasseur") as Ramasseur;
			Vie vie = heros.GetComponent("Vie") as Vie;
			rubisDisplay.text = (ramasseur.rubisCollectés.ToString());
			coeurDisplay.text = (vie.pv + "/" + vie.pvMax);
		}


	}
}
