using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tireur : MonoBehaviour {

	public GameObject projectile;
	public Deplacements deplacements;

	void Update () {
		if (Input.GetKeyDown("space")) Tirer();
	}


	public void Tirer() {
		GameObject poup = Instantiate(projectile, transform.position, Quaternion.identity);
		poup.transform.parent = transform.parent;
		poup.GetComponent<Deplacements>().dir = deplacements.dir;
		poup.GetComponent<Deplacements>().bouge = true;
	}
}