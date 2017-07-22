using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vie : MonoBehaviour {
	[SerializeField]
	private int _pv = 1;
	public int pv
	{
		get { return _pv; }
		// borner les pv entre 0 et pvMax
		set{ _pv = Mathf.Min(Mathf.Max(value, 0), pvMax); }
	}
	// forcer l'inspecteur à utiliser le setter
	void OnValidate()
	{
		pv = _pv;
	}
	public int pvMax = 1;
	public bool invincible = false;



	public void takeDamage(Vector2 direction, float pushPower, float pushTime) {
		if (!invincible)
		{
			pv --;
			Animator animator = gameObject.GetComponent("Animator") as Animator;
			if (animator != null) animator.SetTrigger("Hit");
			// push
			Deplacements deplacements = gameObject.GetComponent("Deplacements") as Deplacements;
			if ( deplacements != null) deplacements.DoPush(direction, pushPower, pushTime);

			if (pv <= 0) die();
		}
	}

	public void die() {
		Conteneur conteneur = gameObject.GetComponent("Conteneur") as Conteneur;
		if ( conteneur != null)
		{
			GameObject dropItem = conteneur.RandomItem();
			if (dropItem != null)
			{
				Instantiate(
					dropItem,
					gameObject.transform.position,
					Quaternion.identity,
					gameObject.transform.parent);
			}
		}
		Destroy(gameObject, 0.1f);
	}
	public void BeInvincible() { invincible = true;  }
	public void BeVulnerable() { invincible = false; }
}
