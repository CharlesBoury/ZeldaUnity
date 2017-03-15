using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	public float vitesse = 0.1f;
	
	[Range(-1, 1)]
	public int horizontal = 0;
	[Range(-1, 1)]
	public int vertical = 0;


	void FixedUpdate () {

		// orientation
		if (vertical   ==  1) gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
		if (vertical   == -1) gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
		if (horizontal == -1) gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
		if (horizontal ==  1) gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);


		if      (horizontal != 0) GetComponent<Rigidbody2D>().velocity = new Vector2 (horizontal * vitesse, 0);
		else if (vertical   != 0) GetComponent<Rigidbody2D>().velocity = new Vector2 (0, vertical * vitesse);
		else                      GetComponent<Rigidbody2D>().velocity = new Vector2 (0,0);
	}
}
