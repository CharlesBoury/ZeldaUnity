using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerveau : MonoBehaviour {

	public Deplacements deplacements;
	public Arme arme;

	void OnDisable()
	{
		deplacements.bouge = false;
	}

	// utilisé en Event (Animation Event || Unity Event)
	public void DisableCerveau()
	{
		this.enabled = false;
	}

	public void EnableCerveau()
	{
		this.enabled = true;
	}

}
