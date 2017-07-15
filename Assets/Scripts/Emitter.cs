using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour {

	public ParticleSystem emitter;
	
	void Trace () {
		emitter.Emit(1);
	}
}
