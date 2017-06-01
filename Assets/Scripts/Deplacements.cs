using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deplacements : MonoBehaviour
{
	public Direction dir;
	public float vitesse;
	public bool bouge;

	Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void OnDisable()
	{
		rb.velocity = new Vector2 (0,0);
	}

	void FixedUpdate ()
	{
		// orientation
		if (dir == Direction.Haut)   gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
		if (dir == Direction.Bas)    gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
		if (dir == Direction.Gauche) gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
		if (dir == Direction.Droite) gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);

		// mouvement
		if (bouge)
		{
			if (dir == Direction.Haut)   rb.velocity = new Vector2 (0, vitesse);
			if (dir == Direction.Bas)    rb.velocity = new Vector2 (0,-vitesse);
			if (dir == Direction.Gauche) rb.velocity = new Vector2 (-vitesse,0);
			if (dir == Direction.Droite) rb.velocity = new Vector2 ( vitesse,0);
		}
		else
		{
			rb.velocity = new Vector2 (0,0);
		}
	}
}
