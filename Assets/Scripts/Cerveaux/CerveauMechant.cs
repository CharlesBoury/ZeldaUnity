using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerveauMechant : Cerveau
{

	public enum State {Move, WaitBeforeAtk, WaitAfterAtk};

	public State state;

	public float perseveranceMin = 0.2f;
	public float perseveranceMax = 1.2f;
	float perseverance;
	public float distanceMinPourTirer;
	[Range(0, 1)]
	public float chanceDeTirer;
	public float attenteAvantTir;
	public float attenteApresTir;

	public Collider2D coll;
	public bool showDebugLines;


	void Start()
	{
		state = State.Move;
		AllerVers(DirectionPossibleAleatoire());
	}


	void Update()
	{
		perseverance -= Time.deltaTime;
		if (perseverance <= 0)
		{
			if      (state == State.WaitBeforeAtk) Atk();
			else if (state == State.WaitAfterAtk) AllerVers(DirectionPossibleAleatoire());
			else if (state == State.Move)
			{
				// Raycast pour voir s'il y a quelque chose devant

				// si un collider est trop près, on ne tirera pas.
				// on teste tous les layers SAUF (~) ceux du joueur et des méchants
				// pour pouvoir tirer si il n'y a rien devant (sauf le joueur ou des méchants)
				int Mask = ~(1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Mechants"));
				// le mask est un chiffre binaire (http://answers.unity3d.com/questions/8715/how-do-i-use-layermasks.html)

				RaycastHit2D hit = Utils.LinecastInDirection(
					coll,
					new Vector2(1,1) * distanceMinPourTirer, // distance en x et en y à tester
					deplacements.dir,
					Mask,
					showDebugLines, Color.red, 0.5F); // display time for debug line

				// s'il y a  assez de distance devant
				// et qu'on choisit de tirer
				if (hit.collider == null && Random.Range(0.0f, 1.0f) <= chanceDeTirer)
				{
					Stop();
				}
				// pas assez de distance devant
				// ou bien pas choisi de tirer
				else
				{
					AllerVers(DirectionPossibleAleatoire());
				}
			}
		}
	}


	void AllerVers(Direction d)
	{
		state = State.Move;
		deplacements.dir = d;
		deplacements.bouge = true;
		RestartPerseverance();
	}


	void Stop()
	{
		state = State.WaitBeforeAtk;
		deplacements.bouge = false;
		perseverance = attenteAvantTir;
	}


	void Atk()
	{
		state = State.WaitAfterAtk;
		arme.Attaquer();
		perseverance = attenteApresTir;
	}


	void OnCollisionEnter2D(Collision2D coll)
	{
		// le cerveau ne réagit aux collisions que s'il est actif
		if (this.enabled) AllerVers(DirectionPossibleAleatoire());
	}


	void RestartPerseverance()
	{
		perseverance = perseveranceMin + (perseveranceMax - perseveranceMin) * Random.Range(0.0f, 1.0f);
	}


	Direction DirectionPossibleAleatoire()
	{
		List<bool> mursPresents = MursPresents(showDebugLines);

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

		List<bool> environnementPresent = new List<bool>();

		// Pour chaque direction
		for (int i = 0; i < 4; i++) {
			// lancer un rayon et voir si on collide avec l'environnement
			// garder cette info dans la liste
			environnementPresent.Add(
				Physics2D.Linecast( // il faut filtrer (enlever) les triggers, see ContactFilter2D
					transform.position + (Utils.DirToVec3(i) * dist) + Utils.RotateZ(Utils.DirToVec3(i),Mathf.PI/2) * size,
					transform.position + (Utils.DirToVec3(i) * dist) + Utils.RotateZ(Utils.DirToVec3(i),-Mathf.PI/2) * size
				)
			);
			// dessiner des lignes de debug dans la scene, vert si rien, rouge si collide
			if (showDebug) Debug.DrawLine(
					transform.position + (Utils.DirToVec3(i) * dist) + Utils.RotateZ(Utils.DirToVec3(i),Mathf.PI/2) * size,
					transform.position + (Utils.DirToVec3(i) * dist) + Utils.RotateZ(Utils.DirToVec3(i),-Mathf.PI/2) * size,
					environnementPresent[i] ? Color.black : Color.green,
					0.5f,
					false);
		}
		return environnementPresent;
	}
}
