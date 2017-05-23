using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour {

	// active/desactive les gameobjects propres à chaque ecran.

	public List<GameObject> screensX0;
	public List<GameObject> screensX1;

	public CameraFollow cam;

	Cerveau[] cerveaux;

	void Update () {
		if (cam != null)
		{
			for(int i = 0; i < 2; i++)
			{
				for(int j = 0; j < 2; j++)
				{
					bool isActive = cam.gridX == i && cam.gridY == j;
					if ( i == 0 ) cerveaux = screensX0[j].GetComponentsInChildren<Cerveau>();
					else          cerveaux = screensX1[j].GetComponentsInChildren<Cerveau>();
					foreach (Cerveau c in cerveaux) c.enabled = isActive;
				}
			}
		}
	}
}