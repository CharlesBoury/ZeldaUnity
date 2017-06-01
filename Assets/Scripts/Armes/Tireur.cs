using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tireur : Arme {

	public GameObject projectile;
	public Deplacements deplacements;


	public override void Attaquer() {
		GameObject poup = Instantiate(projectile, transform.position, Quaternion.identity);
		poup.transform.parent = transform.parent;
		poup.tag = gameObject.tag;
		poup.GetComponent<Deplacements>().dir = deplacements.dir;
		poup.GetComponent<Deplacements>().bouge = true;
	}
}