using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerveau : MonoBehaviour {

	public Deplacements deplacements;

	public virtual void Start()
	{
		deplacements = GetComponent<Deplacements>();
	}

	void OnDisable()
	{
		deplacements.bouge = false;
	}
}
