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
}
