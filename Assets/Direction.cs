using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour {

	public float vitesse = 0.1f;
	
	[Range(-1, 1)]
	public int horizontal = 0;
	[Range(-1, 1)]
	public int vertical = 0;


	void Update () {

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

		// recompute vitesse if diagonal
		// float vit = vitesse;
		// if (vertical != 0 && horizontal != 0) vit *= Mathf.Cos(Mathf.PI/4);

		if (horizontal != 0)
			transform.localPosition = new Vector3(
				transform.localPosition.x + horizontal * vitesse * Time.deltaTime,
				transform.localPosition.y,
				transform.localPosition.z);
		else if (vertical != 0)
			transform.localPosition = new Vector3(
				transform.localPosition.x,
				transform.localPosition.y + vertical * vitesse * Time.deltaTime,
				transform.localPosition.z);
	}
}
