using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	public float vitesse = 0.1f;
	
	[Range(-1, 1)]
	public int horizontal = 0;
	[Range(-1, 1)]
	public int vertical = 0;

	public Cerveau cerveau;

	void OnDisable(){
		GetComponent<Rigidbody2D>().velocity = new Vector2 (0,0);
	}

	void FixedUpdate () {

		// s'il y a un cerveau aux commandes, alors piloter les commandes
		if (cerveau != null && cerveau.enabled)
		{
			if      (cerveau.down && cerveau.up) vertical =  0;
			else if (cerveau.down              ) vertical = -1;
			else if (                cerveau.up) vertical =  1;
			else                                 vertical =  0;


			if      (cerveau.left && cerveau.right) horizontal =  0;
			else if (cerveau.left                 ) horizontal = -1;
			else if (                cerveau.right) horizontal =  1;
			else                                    horizontal =  0;
		}
		else
		{
			horizontal = 0;
			vertical   = 0;
		}

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
