using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramasseur : MonoBehaviour {

	public int rubisCollectés = 0;

	void OnTriggerEnter2D(Collider2D coll) {
		GameObject truc = coll.gameObject;
		Rubis ru = truc.GetComponent("Rubis") as Rubis;
		if (ru != null) {
			rubisCollectés += ru.valeur;
			Destroy(truc);
		}
	}
}
