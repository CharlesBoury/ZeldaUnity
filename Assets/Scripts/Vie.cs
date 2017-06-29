using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vie : MonoBehaviour {

	public int pv = 1;
	public int pvMax = 1;

	public GameObject destroyable;

	public void takeDamage(Vector2 direction, float pushPower, float pushTime) {
		pv --;
		Deplacements deplacements = gameObject.GetComponent("Deplacements") as Deplacements;
		if ( deplacements != null) deplacements.DoPush(direction, pushPower, pushTime);
		if (pv <= 0) die();
	}

	public void die() {
		Conteneur conteneur = gameObject.GetComponent("Conteneur") as Conteneur;
		if ( conteneur != null)
		{
			Instantiate(
				conteneur.contenu,
				gameObject.transform.position,
				Quaternion.identity);
		}
		if (destroyable != null) Destroy(destroyable, 0.1f);
	}
}
