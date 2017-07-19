using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpeeRamassable : MonoBehaviour
{
	public GameObject aGagner;
	public GameObject papi;

	void OnTriggerEnter2D(Collider2D collider2D)
	{
		if (this.enabled
		&& collider2D.gameObject.layer == LayerMask.NameToLayer("Player")
		&& collider2D.isTrigger)
		{
			Cerveau c = collider2D.gameObject.GetComponent <Cerveau>();
			if (c!= null)
			{
				// get Epee
				GameObject poup = Instantiate(aGagner, collider2D.transform);
				poup.name = "Epee";
				c.arme = aGagner.GetComponent<Epee>();
				Destroy(gameObject);

				// faire disparaitre papi
				papi.GetComponent<Animator>().SetTrigger("Disparition");

				// jouer anim GetSword
				collider2D.gameObject.GetComponent <Animator>().SetTrigger("GetSword");
				collider2D.gameObject.GetComponent <Animator>().Rebind();
			}
		}
	}
}
