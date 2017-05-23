using UnityEngine;
using System.Collections;

public class Cerveau : MonoBehaviour
{
	public bool up;
	public bool down;
	public bool left;
	public bool right;

	public void Start()
	{
		ResetDirections();
	}

	public void ResetDirections() {
		up    = false;
		down  = false;
		left  = false;
		right = false;
	}
}