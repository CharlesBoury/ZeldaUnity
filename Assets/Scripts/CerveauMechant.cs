using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerveauMechant : MonoBehaviour {
	public bool up;
	public bool down;
	public bool left;
	public bool right;

	public float attenteQuandImmobile;
	public float perseveranceMin;
	public float perseveranceMax;
	float perseverance;
	[Range(0, 1)]
	public float chanceDeSarreter;

	public bool showLineCast;
	public float distLinecast;



	void Start()
	{
		restartPerseverance();
	}

	void restartPerseverance()
	{
		perseverance = perseveranceMin + (perseveranceMax - perseveranceMin)*Random.Range(0.0f, 1.0f);
	}


	void OnCollisionEnter2D(Collision2D coll) {
		restartPerseverance();
		ChangeDirection();
	}

	void Update ()
	{
		perseverance -= Time.deltaTime;
		if (perseverance <= 0)
		{
			// 30% chance de s'arreter
			if (Random.Range(0.0f, 1.0f) <= chanceDeSarreter)
			{
				Reset();
				perseverance = attenteQuandImmobile;
			}
			else
			{
				ChangeDirection();
				restartPerseverance();
			}
		}
		piloteMove();
	}

	void ChangeDirection() {
		List<bool> mursPresents = MursPresents(distLinecast, showLineCast);

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

	List<bool> MursPresents(float dist, bool showDebug = false) {

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
					transform.position,
					transform.position + (directions[i] * dist)
				)
			);
			// dessiner des lignes de debug dans la scene, vert si rien, rouge si collide
			if (showDebug) Debug.DrawLine(
					transform.position,
					transform.position + (directions[i] * dist), 
					environnementPresent[i] ? Color.black : Color.green,
					0.5f,
					false);
		}
		return environnementPresent;
	}

	void piloteMove()
	{
		// piloter les valeurs du component Move
		Move m = GetComponent<Move>();
		if      (down && up) m.vertical =  0;
		else if (down      ) m.vertical = -1;
		else if (        up) m.vertical =  1;
		else                 m.vertical =  0;


		if      (left && right) m.horizontal =  0;
		else if (left         ) m.horizontal = -1;
		else if (        right) m.horizontal =  1;
		else                    m.horizontal =  0;
	}

	void AppuyerSurBoutons(int n) {
		up    = (n==0);
		down  = (n==1);
		left  = (n==2);
		right = (n==3);
	}

	void Reset() {
		up    = false;
		down  = false;
		left  = false;
		right = false;
	}
}
