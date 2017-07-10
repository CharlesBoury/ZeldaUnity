using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AssombrirJoueur : MonoBehaviour {

	public AnimationCurve ease;
	BoxCollider2D myBox;

	void Start()
	{
		myBox = GetComponent<BoxCollider2D>();
	}
	void OnTriggerStay2D(Collider2D collider2D)
	{
		if (collider2D.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			SpriteRenderer sprite = collider2D.gameObject.GetComponent<SpriteRenderer>();
			if (sprite != null)
			{
				// percent : de combien est rentré le joueur dans le trigger PAR LE BAS
				float dist = collider2D.transform.position.y - myBox.bounds.min.y;
				float percent = dist/myBox.bounds.size.y;

				// on transforme ce pourcentage en couleur : 0% = blanc, 100% = noir
				// en prenant en compte la courbe (0 = blanc / 1 = noir)
				percent = ease.Evaluate(percent);
				sprite.color = new Color(1-percent, 1-percent, 1-percent, 1F);
			}
		}
	}
}
