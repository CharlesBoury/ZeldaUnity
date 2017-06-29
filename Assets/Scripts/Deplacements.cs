using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Deplacements : MonoBehaviour
{
	public Direction dir;
	public bool bouge;
	public float vitesse;
	[Range(0, 2)]
	public float vMult = 1; // pour ne pas marcher avec une vitesse linéaire

	// infos d'un eventuel push
	public float pushTimer;
	public Vector2 pushDir;
	public float pushPower;

	Rigidbody2D rb;
	Animator animator;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	public void DoPush(Vector2 direction, float puissance, float temps)
	{
		pushDir = direction;
		pushPower = puissance;
		pushTimer = temps;
	}

	void FixedUpdate ()
	{
		if (animator != null)
		{
			animator.SetFloat("Direction", (float)dir);
			animator.SetBool("Bouge", bouge);
		}

		// mouvement
		if (pushTimer > 0)
		{
			pushTimer -= Time.fixedDeltaTime;

			// Vector2 vel = new Vector2();
			// if (pushDir == Direction.Haut)   vel = new Vector2 (0, pushPower);
			// if (pushDir == Direction.Bas)    vel = new Vector2 (0,-pushPower);
			// if (pushDir == Direction.Gauche) vel = new Vector2 (-pushPower,0);
			// if (pushDir == Direction.Droite) vel = new Vector2 ( pushPower,0);
			rb.MovePosition(rb.position + pushDir * pushPower * Time.fixedDeltaTime);
		}

		else if (bouge)
		{
			Vector2 vel = new Vector2();
			if (dir == Direction.Haut)   vel = new Vector2 (0, vitesse);
			if (dir == Direction.Bas)    vel = new Vector2 (0,-vitesse);
			if (dir == Direction.Gauche) vel = new Vector2 (-vitesse,0);
			if (dir == Direction.Droite) vel = new Vector2 ( vitesse,0);
			rb.MovePosition(rb.position + vel * vMult * Time.fixedDeltaTime);
		}
		else rb.velocity = new Vector2(0,0);

	}
}
