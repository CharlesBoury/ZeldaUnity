using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public Vie vie;
	public Ramasseur ramasseur;
	public Text rubisDisplay;
	public Text coeurDisplay;

	void Update () {

		// si le héros n'est pas mort
		if (vie != null) coeurDisplay.text = (vie.pv + "/" + vie.pvMax);
		if (ramasseur != null) rubisDisplay.text = (ramasseur.rubisCollectés.ToString());


	}
}
