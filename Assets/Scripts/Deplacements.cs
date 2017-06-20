using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deplacements : MonoBehaviour
{
	public Direction dir;
	public float vitesse;
	[Range(0, 2)]
	public float vMult = 1;
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
		if (animator != null)
		{
			animator.SetFloat("Direction", (float)dir);
			animator.SetBool("Bouge", bouge);
		}
		// mouvement
		if (bouge)
		{
			if (dir == Direction.Haut)   rb.velocity = new Vector2 (0, vitesse * vMult);
			if (dir == Direction.Bas)    rb.velocity = new Vector2 (0,-vitesse * vMult);
			if (dir == Direction.Gauche) rb.velocity = new Vector2 (-vitesse * vMult,0);
			if (dir == Direction.Droite) rb.velocity = new Vector2 ( vitesse * vMult,0);
		}
		else
		{
			rb.velocity = new Vector2 (0,0);
		}
	}
}
