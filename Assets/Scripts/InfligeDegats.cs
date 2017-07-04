using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfligeDegats : MonoBehaviour
{

	public float pushPower = 10;
	public float pushTime = 0.1f;

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
			vie.takeDamage(
				(Vector2)((hitColl.transform.position - transform.position) + (Vector3)hitColl.offset).normalized,
				pushPower,
				pushTime
			);
		}
	}
}
