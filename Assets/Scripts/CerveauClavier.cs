using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerveauClavier : Cerveau {

	void Update ()
	{
		up    = Input.GetKey("up");
		down  = Input.GetKey("down");
		left  = Input.GetKey("left");
		right = Input.GetKey("right");
	}
}
