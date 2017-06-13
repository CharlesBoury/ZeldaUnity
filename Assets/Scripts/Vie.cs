using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vie : MonoBehaviour {

	public int pv = 1;
	public int pvMax = 1;

	public GameObject destroyable;

	void Start() {
		pv = pvMax;
	}

	public void takeDamage() {
		pv --;
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
		if (destroyable != null) Destroy(destroyable);
	}
}
