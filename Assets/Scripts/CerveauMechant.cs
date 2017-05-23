using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerveauMechant : Cerveau
{

	public float attenteQuandImmobile;
	public float perseveranceMin;
	public float perseveranceMax;
	float perseverance;

	[Range(0, 1)]
	public float chanceDeSarreter;

	public bool showLineCast;
	public float distLinecast;
	public float sizeLinecast;



	new void Start()
	{
		ResetDirections();
		RestartPerseverance();
	}

	void RestartPerseverance()
	{
		perseverance = perseveranceMin + (perseveranceMax - perseveranceMin) * Random.Range(0.0f, 1.0f);
	}


	void OnCollisionEnter2D(Collision2D coll) {
		if (this.enabled) // le cerveau ne réagit aux collisions que s'il est actif
		{
			ChangeDirection();
			RestartPerseverance();
		}
	}

	void Update ()
	{
		perseverance -= Time.deltaTime;
		if (perseverance <= 0)
		{
			// 30% chance de s'arreter
			if (Random.Range(0.0f, 1.0f) <= chanceDeSarreter)
			{
				ResetDirections();
				perseverance = attenteQuandImmobile;
			}
			else
			{
				ChangeDirection();
				RestartPerseverance();
			}
		}
	}

	void ChangeDirection() {
		List<bool> mursPresents = MursPresents(distLinecast, sizeLinecast, showLineCast);

		// compte le nombre de murs absents (combien de false dans la liste)
		int possibilites = 0;
		for(int i = 0; i < mursPresents.Count; i++) {
			possibilites += mursPresents[i] ? 0 : 1;
		}
		// s'il y a 2 murs absents, on choisit un nombre entre 1 et 2.
		int dice = Random.Range(1,possibilites+1); // le +1 est là pour que ce soit inclusif

		// choix de la direction parmis celles qui sont libres
		for (int i = 0; i < mursPresents.Count; i++) {
			if (!mursPresents[i]) { // si libre
				dice --;
				if (dice == 0) {
					AppuyerSurBoutons(i);
					break;
				}
			}
		}
	}

	List<bool> MursPresents(float dist, float size, bool showDebug = false) {

		// renvoie une liste de bool, dans le même ordre
		// que les directions: haut, bas, gauche, droite.
		// true si un mur est present.

		List<Vector3> directions = new List<Vector3>();
		directions.Add(Vector3.up);
		directions.Add(Vector3.down);
		directions.Add(Vector3.left);
		directions.Add(Vector3.right);

		List<bool> environnementPresent = new List<bool>();

		// Pour chaque direction
		for (int i = 0; i < 4; i++) {
			// lancer un rayon et voir si on collide avec l'environnement
			// garder cette info dans la liste
			environnementPresent.Add(
				Physics2D.Linecast(
					transform.position + (directions[i] * dist) + Utils.RotateZ(directions[i],Mathf.PI/2) * size,
					transform.position + (directions[i] * dist) + Utils.RotateZ(directions[i],-Mathf.PI/2) * size
				)
			);
			// dessiner des lignes de debug dans la scene, vert si rien, rouge si collide
			if (showDebug) Debug.DrawLine(
					transform.position + (directions[i] * dist) + Utils.RotateZ(directions[i],Mathf.PI/2) * size,
					transform.position + (directions[i] * dist) + Utils.RotateZ(directions[i],-Mathf.PI/2) * size,
					environnementPresent[i] ? Color.black : Color.green,
					0.5f,
					false);
		}
		return environnementPresent;
	}

	void AppuyerSurBoutons(int n) {
		up    = (n==0);
		down  = (n==1);
		left  = (n==2);
		right = (n==3);
	}
}
