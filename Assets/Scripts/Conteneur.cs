using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conteneur : MonoBehaviour {

	public List<GameObject> dropItems;


	public GameObject RandomItem()
	{
		int t = dropItems.Count;
		float randomValue = Random.value * t;
		for(int i = 0; i < t; i++)
		{
			// distribution equivalente des chances entre tous les dropItems
			if (i <= randomValue && randomValue <= i+1)
			{
				// Debug.Log("drop item "+ (i+1));
				return dropItems[i];
			}
		}
		return null; // cas impossible, mais sinon ca compile pas
	}
}