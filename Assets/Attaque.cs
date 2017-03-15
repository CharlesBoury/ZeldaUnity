using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attaque : MonoBehaviour {

	public GameObject epee;
	
	void Update () {
		if (Input.GetKeyDown("space")) {
			epee.SetActive(true);
		} else {
			epee.SetActive(false);
		}
	}
}
