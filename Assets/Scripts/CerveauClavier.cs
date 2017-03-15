using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerveauClavier : MonoBehaviour {
	public bool up;
	public bool down;
	public bool left;
	public bool right;

	void Update () {
		up    = Input.GetKey("up");
		down  = Input.GetKey("down");
		left  = Input.GetKey("left");
		right = Input.GetKey("right");


		// piloter les valeurs du component Move
		Move m = GetComponent<Move>();
		if      (down && up) m.vertical =  0;
		else if (down      ) m.vertical = -1;
		else if (        up) m.vertical =  1;
		else                 m.vertical =  0;


		if      (left && right) m.horizontal =  0;
		else if (left         ) m.horizontal = -1;
		else if (        right) m.horizontal =  1;
		else                    m.horizontal =  0;
	}
}
