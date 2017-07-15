using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporteur : MonoBehaviour {

	public Transform whereToTeleport;
	public CameraFollow cameraFollow;

	void OnTriggerEnter2D(Collider2D collider2D)
	{
		if (collider2D.gameObject.layer == LayerMask.NameToLayer("Player") && !collider2D.isTrigger)
		{
			cameraFollow.placeToTeleport = whereToTeleport.position;
			cameraFollow.GetComponent<Animator>().SetTrigger("Teleport");
		}
	}
}
