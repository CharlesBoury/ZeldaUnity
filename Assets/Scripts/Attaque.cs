using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attaque : MonoBehaviour {

	public GameObject epee;
	
	void Update () {
		if (Input.GetKeyDown("space")) {
			Debug.Log("Attak !");
			epee.SetActive(true)	;
		} else {
			epee.SetActive(false);
		}
	}
}
