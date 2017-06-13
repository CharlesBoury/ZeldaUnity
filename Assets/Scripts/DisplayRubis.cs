using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class DisplayRubis : MonoBehaviour {

	public Ramasseur ramasseur;
	public Text display;

	void Update()
	{
		if (ramasseur != null) display.text = (ramasseur.rubisCollectés.ToString());
	}
}
