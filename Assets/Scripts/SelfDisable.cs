using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDisable : MonoBehaviour {

	public float compteARebour = 0.1f;
	float i;

	void Start () {
		i = compteARebour;
	}
	
	// Update is called once per frame
	void Update () {
		i -= Time.deltaTime;
		if (i <= 0)
		{
			gameObject.SetActive(false);
			i = compteARebour;
		}
	}
}
