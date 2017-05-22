using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform cible;

	public Camera cam;
	float cameraL;
	float cameraH;

	public int gridX = 0;
	public int gridY = 0;

	public Animator animator;

	void Start(){
		cameraL = cam.orthographicSize * 2 * cam.aspect;
		cameraH = cam.orthographicSize * 2;
	}
	
	void Update () {
		// s'il y a une cible
		if (cible != null) {
			gridX = Mathf.FloorToInt(cible.localPosition.x/cameraL);
			gridY = Mathf.FloorToInt(cible.localPosition.y/cameraH);
		}

		animator.SetInteger("X", gridX); 
		animator.SetInteger("Y", gridY); 
	}
}
