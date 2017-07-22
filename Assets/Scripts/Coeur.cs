using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coeur : MonoBehaviour {

	public int valeur = 1;
	private bool ramasssable = true; // pour eviter plusieurs ramassages dans la même frame

	void OnTriggerEnter2D(Collider2D coll)
	{
		// les coeurs ne sont ramassables que par les triggers du joueur
		if (ramasssable && coll.isTrigger && coll.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			Vie v = coll.gameObject.GetComponentInParent(typeof(Vie)) as Vie;
			if (v != null)
			{
				ramasssable = false;
				v.pv += valeur;
				Destroy(gameObject);
			}
		}
	}
}
