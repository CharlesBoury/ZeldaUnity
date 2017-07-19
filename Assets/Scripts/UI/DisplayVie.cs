using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class DisplayVie : MonoBehaviour {

	public Vie vie;
	public Text display;

	void Update()
	{
		if (vie != null) display.text = vie.pv + "/"+ vie.pvMax;
	}
}
