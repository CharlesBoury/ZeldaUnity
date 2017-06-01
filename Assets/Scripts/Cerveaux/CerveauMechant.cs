using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerveauMechant : Cerveau
{
	public float perseveranceMin = 0.2f;
	public float perseveranceMax = 1.2f;
	float perseverance;
	[Range(0, 1)]
	public float chanceDeSarreter;
	public float attenteQuandImmobile;

	public Collider2D coll;
	public bool showLineCast;


	public override void Start()
	{
		base.Start();
		AllerVers(DirectionPossibleAleatoire());
	}


	void Update()
	{
		perseverance -= Time.deltaTime;
		if (perseverance <= 0)
		{
			// 30% chance de s'arreter
			if (Random.Range(0.0f, 1.0f) <= chanceDeSarreter)
			{
				Stop();
			}
			else
			{
				AllerVers(DirectionPossibleAleatoire());
			}
		}
	}


	void AllerVers(Direction d)
	{
		deplacements.dir = d;
		deplacements.bouge = true;
		RestartPerseverance();
	}


	void OnCollisionEnter2D(Collision2D coll)
	{
		// le cerveau ne réagit aux collisions que s'il est actif
		if (this.enabled) AllerVers(DirectionPossibleAleatoire());
	}


	void Stop()
	{
		deplacements.bouge = false;
		perseverance = attenteQuandImmobile;
	}


	void RestartPerseverance()
	{
		perseverance = perseveranceMin + (perseveranceMax - perseveranceMin) * Random.Range(0.0f, 1.0f);
	}


	Direction DirectionPossibleAleatoire()
	{
		List<bool> mursPresents = MursPresents(showLineCast);

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
					return (Direction)i;
				}
			}
		}

		// Et s'il n'y avait aucune direction libre:
		return Direction.Haut;
	}


	List<bool> MursPresents(bool showDebug = false) {
		float size = coll.bounds.extents.x; // on assume que c'est un carré.
		float dist = size + 0.1f;

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
}
