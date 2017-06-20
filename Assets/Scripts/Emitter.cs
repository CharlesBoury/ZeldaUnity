using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour {

	public ParticleSystem particleSystem;
	
	void Trace () {
		particleSystem.Emit(1);
	}
}
