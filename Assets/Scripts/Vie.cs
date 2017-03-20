using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vie : MonoBehaviour {

	public int pv = 1;
	public int pvMax = 1;
	public TextMesh display;

	void Start() {
		pv = pvMax;
		displayVie();
	}

	public void takeDamage() {
		pv --;
		displayVie();
		if (pv <= 0) die();
	}

	public void die() {
		Destroy(gameObject);
	}

	public void displayVie() {
		if (display != null) {
			display.text = pv+"/"+pvMax;
		}
	}
}
