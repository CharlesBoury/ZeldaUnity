using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

	public Transform cible;

	void Start () {
		
	}
	
	void Update () {
		Camera c = GetComponent<Camera>();

		if (cible.localPosition.y - transform.localPosition.y >= c.orthographicSize) {
			transform.localPosition = new Vector3(
				transform.localPosition.x,
				transform.localPosition.y + c.orthographicSize * 2,
				transform.localPosition.z);
		}
		if (cible.localPosition.y - transform.localPosition.y <= -c.orthographicSize) {
			transform.localPosition = new Vector3(
				transform.localPosition.x,
				transform.localPosition.y - c.orthographicSize * 2,
				transform.localPosition.z);
		}
		if (cible.localPosition.x - transform.localPosition.x >= c.orthographicSize * c.aspect) {
			transform.localPosition = new Vector3(
				transform.localPosition.x + c.orthographicSize * c.aspect * 2,
				transform.localPosition.y,
				transform.localPosition.z);
		}
		if (cible.localPosition.x - transform.localPosition.x <= -c.orthographicSize * c.aspect) {
			Debug.Log("Blarp");
			transform.localPosition = new Vector3(
				transform.localPosition.x - c.orthographicSize * c.aspect * 2,
				transform.localPosition.y,
				transform.localPosition.z);
		}
	}
}
