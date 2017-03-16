using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerveauMechant : MonoBehaviour {

	public bool up;
	public bool down;
	public bool left;
	public bool right;

	public float rythmeDeChgmnt = 2f;
	float remaining = 0;

	void Update () {
		remaining -= Time.deltaTime;
		if (remaining <= 0) {
			ChooseNewDirection();
			remaining = rythmeDeChgmnt;
		}


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


	void ChooseNewDirection () {
		Reset();
		// 33% chance of moving vertically
		if (Random.value < 0.33f) {
			// which vertical direciton (50/50)
			if (Random.value > 0.5f) up = true;
			else down = true;
		} else if (Random.value < 0.66f) {
			// which horizontal direciton (50/50)
			if (Random.value > 0.5f) left = true;
			else right = true;
		} else {
			Reset();
		}
	}

	void Reset() {
		up    = false;
		down  = false;
		left  = false;
		right = false;
	}
}
