using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfligeDegats : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D coll) {
		// QUAND LE TRIGGER DE L'ARME TOUCHE LE COLLIDER DE QUELQU'UN

		// atk uniquement si pas de la meme famille
		if (gameObject.tag != coll.gameObject.tag
		// et si l'objet touché n'est pas trigger
		&& !coll.isTrigger) {

			// et si celui touché a une vie
			Vie targetLife = coll.gameObject.GetComponent("Vie") as Vie;
			if (targetLife != null) {

				Debug.Log(gameObject.tag + " atk=> " + coll.gameObject.tag);
				targetLife.takeDamage();
			}
		}
	}
}
