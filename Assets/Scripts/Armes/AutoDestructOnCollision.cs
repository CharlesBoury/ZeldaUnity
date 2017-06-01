using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestructOnCollision : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll)
	{
		// ne se détruit pas sur les triggers
		if (!coll.isTrigger) Destroy(gameObject);
	}
}
