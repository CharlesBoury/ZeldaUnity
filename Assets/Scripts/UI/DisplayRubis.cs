using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class DisplayRubis : MonoBehaviour {

	public BourseARubis bourseARubis;
	public Text display;

	void Update()
	{
		if (bourseARubis != null) display.text = (bourseARubis.rubisCollectés.ToString());
	}
}
