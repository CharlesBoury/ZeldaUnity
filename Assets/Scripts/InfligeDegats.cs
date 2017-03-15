using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfligeDegats : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			Destroy (coll.gameObject);
		}
	}
}
