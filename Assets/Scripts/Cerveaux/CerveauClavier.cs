using UnityEngine;
using System.Collections;

public class CerveauClavier : Cerveau
{
	public bool up;
	public bool down;
	public bool left;
	public bool right;

	void Update ()
	{
		up    = Input.GetKey("up");
		down  = Input.GetKey("down");
		left  = Input.GetKey("left");
		right = Input.GetKey("right");

		// ------------------------------------------------
		// filtrage des inputs

		// on ne change pas de direction si
		// 2 touches opposées sont pressées
		if (!(up && down))
		{
			if      (up  ) deplacements.dir = Direction.Haut;
			else if (down) deplacements.dir = Direction.Bas;
		}

		if (!(left && right))
		{
			if      (left ) deplacements.dir = Direction.Gauche;
			else if (right) deplacements.dir = Direction.Droite;
		}

		// on bouge que si
		deplacements.bouge =
			// au moins une touche est pressée
			(up || down || left || right)
			// mais pas si 2 touches opposées sont pressées
		 	&& !(up && down)
			&& !(left && right);
	}
}