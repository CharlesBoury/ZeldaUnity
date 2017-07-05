using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Grotte : MonoBehaviour {

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
				float dist = myBox.bounds.max.y - collider2D.transform.position.y;
				float percent = dist/myBox.bounds.size.y;
				percent = ease.Evaluate(percent);
					sprite.color = new Color(percent, percent, percent, 1F);
			}
		}
	}
}
