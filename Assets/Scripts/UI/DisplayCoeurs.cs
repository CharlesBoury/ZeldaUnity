using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class DisplayCoeurs : MonoBehaviour {

	public Vie vie;
	public Sprite coeurVide;
	public Sprite coeurMoitie;
	public Sprite coeurPlein;

	public GameObject coeurPrefab;

	int currentPvMax = 0;

	void Update()
	{
		if (currentPvMax != vie.pvMax)
		{
			modifierNombreDeCoeurs();
			currentPvMax = vie.pvMax;
		}

		for(int i = 1; i <= vie.pvMax; i += 2) // 1,3,5...
		{
			if (transform.childCount > (i-1)/2) // sinon error quand pv > pvMax
			{
				if (vie.pv <  i) transform.GetChild((i-1)/2).GetComponent<Image>().sprite = coeurVide;
				if (vie.pv == i) transform.GetChild((i-1)/2).GetComponent<Image>().sprite = coeurMoitie;
				if (vie.pv >  i) transform.GetChild((i-1)/2).GetComponent<Image>().sprite = coeurPlein;
			}
		}
	}

	void modifierNombreDeCoeurs()
	{
		int coeursNecessaires = Mathf.CeilToInt(vie.pvMax/2.0F);
		// Debug.Log("----------------------");
		// Debug.Log(coeursNecessaires + " coeursNecessaires.");
		if (coeursNecessaires < transform.childCount)
		{
			for(int i = transform.childCount - 1; i > coeursNecessaires - 1; i --)
			{
				// Debug.Log("destroy Child "+i);
				GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
			}
		}
		else if (coeursNecessaires > transform.childCount)
		{
			for(int i = transform.childCount ; i < coeursNecessaires; i ++)
			{
				// Debug.Log("Création du coeur numéro "+i);
				Instantiate(coeurPrefab, transform);
			}
		}
	}
}
