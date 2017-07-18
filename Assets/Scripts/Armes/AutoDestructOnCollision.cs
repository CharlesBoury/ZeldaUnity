using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestructOnCollision : MonoBehaviour {

	bool trig;

	void OnTriggerEnter2D(Collider2D coll)
	{

		trig = true;
	}

	void LateUpdate()
	{
		// on destroy dans LateUpdate() pour laisser le temps
		// à InfligeDegats d'attaquer
		if (trig) Destroy(gameObject);
	}
}
