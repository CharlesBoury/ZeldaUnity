using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour {

	public float vitesse = 0.1f;
	
	[Range(-1, 1)]
	public int horizontal = 0;
	[Range(-1, 1)]
	public int vertical = 0;


	void FixedUpdate () {

		bool up    = Input.GetKey("up");
		bool down  = Input.GetKey("down");
		bool left  = Input.GetKey("left");
		bool right = Input.GetKey("right");

		// choose vertical and horizontal signs
		if      (down && up) vertical =  0;
		else if (down      ) vertical = -1;
		else if (        up) vertical =  1;
		else                 vertical =  0;


		if      (left && right) horizontal =  0;
		else if (left         ) horizontal = -1;
		else if (        right) horizontal =  1;
		else                    horizontal =  0;

		// orientation
		if (up)    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
		if (down)  gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
		if (left)  gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
		if (right) gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);


		if      (horizontal != 0) GetComponent<Rigidbody2D>().velocity = new Vector2 (horizontal * vitesse, 0);
		else if (vertical   != 0) GetComponent<Rigidbody2D>().velocity = new Vector2 (0, vertical * vitesse);
		else                      GetComponent<Rigidbody2D>().velocity = new Vector2 (0,0);
	}
}
