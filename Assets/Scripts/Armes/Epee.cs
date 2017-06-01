using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Epee : Arme {

	public GameObject epee;
	
	public override void Attaquer ()
	{
		epee.SetActive(true);
	}
}
