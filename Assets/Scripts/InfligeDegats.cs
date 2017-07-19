using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfligeDegats : MonoBehaviour
{

	public Transform pushFrom;
	public float pushPower = 10;
	public float pushTime = 0.1f;

	void Awake()
	{
		if (pushFrom == null) pushFrom = transform;
	}

	void OnTriggerEnter2D(Collider2D collider2D)
	{
		// Quand le trigger du infligeDegats touche un trigger HitBox d'un autre layer
		if (this.gameObject.layer != collider2D.gameObject.layer && collider2D.tag == "HitBox" && collider2D.isTrigger)
		{
			// Debug.Log("trigger de "+ gameObject.transform.name+" touche trigger hitbox de "+collider2D.transform.name);
			Attaque(collider2D);
		}
	}

	void Attaque(Collider2D hitColl)
	{
		Vie vie = hitColl.gameObject.GetComponent<Vie>();
		if (vie != null)
		{
			vie.takeDamage(
				(Vector2)((hitColl.transform.position - pushFrom.position) + (Vector3)hitColl.offset).normalized,
				pushPower,
				pushTime
			);
		}
	}
}
