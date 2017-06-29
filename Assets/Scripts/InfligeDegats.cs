using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfligeDegats : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D collider2D)
	{
		// Quand le trigger du infligeDegats touche une HitBox d'un autre layer
		if (this.gameObject.layer != collider2D.gameObject.layer && collider2D.tag == "HitBox")
		{
			// Debug.Log("trigger de "+ gameObject.transform.name+" touche hitbox de "+collider2D.transform.name);
			Attaque(collider2D, gameObject.layer);
		}
	}

	void OnCollisionEnter2D(Collision2D hit)
	{
		// Quand le collider (non trigger) du infligeDegats touche une HitBox (non trigger) d'un autre layer
		if (this.gameObject.layer != hit.gameObject.layer && hit.collider.tag == "HitBox")
		{
			// Debug.Log("collider de "+ gameObject.transform.name+" touche collider hitbox de "+ hit.gameObject.transform.name);
			Attaque(hit.collider, gameObject.layer);
		}
	}

	void Attaque(Collider2D hitColl, int layerAttaquant)
	{
		Vie vie = hitColl.gameObject.GetComponent<Vie>();
		if (vie != null)
		{
			// choix de la puissance de recul
			float pushPower; float pushTime;
			if      (layerAttaquant == LayerMask.NameToLayer("Player"))      {pushPower = 13; pushTime = 0.17f;}
			else if (layerAttaquant == LayerMask.NameToLayer("Projectiles")) {pushPower =  8; pushTime = 0.10f;}
			else                                                             {pushPower = 11; pushTime = 0.15f;}

			vie.takeDamage(
				(Vector2)((hitColl.transform.position - transform.position) + (Vector3)hitColl.offset).normalized,
				pushPower,
				pushTime
			);
		}
	}
}
