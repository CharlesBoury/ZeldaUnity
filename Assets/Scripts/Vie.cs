using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vie : MonoBehaviour {

	public int pv = 1;
	public int pvMax = 1;
	public TextMesh display;

	void Start() {
		pv = pvMax;
		displayVie();
	}

	public void takeDamage() {
		pv --;
		displayVie();
		if (pv <= 0) die();
	}

	public void die() {
		Conteneur conteneur = gameObject.GetComponent("Conteneur") as Conteneur;
		if ( conteneur != null) {
			Instantiate(
				conteneur.contenu,
				gameObject.transform.position,
				Quaternion.identity);
		}
		Destroy(gameObject, 0.1f);
	}

	public void displayVie() {
		if (display != null) {
			display.text = pv+"/"+pvMax;
		}
	}
}
