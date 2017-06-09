using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deplacements : MonoBehaviour
{
	public Direction dir;
	public float vitesse;
	public bool bouge;

	Rigidbody2D rb;
	Animator animator;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	void OnDisable()
	{
		rb.velocity = new Vector2 (0,0);
	}

	void FixedUpdate ()
	{
		if (animator != null) animator.SetFloat("Direction", (float)dir);
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
