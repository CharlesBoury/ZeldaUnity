using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vie : MonoBehaviour {

	public int pv = 1;
	public int pvMax = 1;
	public TextMesh display;

	void Start() {
		pv = pvMax;
		display.text = pv+"/"+pvMax;
	}

	public void takeDamage() {
		pv --;
		display.text = pv+"/"+pvMax;
		if (pv <= 0) die();
	}

	public void die() {
		Destroy(gameObject);
	}
}
