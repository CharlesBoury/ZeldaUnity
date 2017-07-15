using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionState : StateMachineBehaviour {

	public CameraFollow cf;
	Rigidbody2D rbCible;
	public Direction direction;
	float previous;
	float goal;


	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// save previousPos de la cible pour le bouger relativement
		if ((int)direction < 2) previous = cf.cible.position.y;
		else                    previous = cf.cible.position.x;

		// calcul du goal de la position de la cible
		Collider2D box = cf.cibleVisibleBox;
		if (direction == Direction.Haut  ) goal = (cf.grid.y+1) * cf.camSize.y +  box.bounds.extents.y - box.offset.y + 0.1F;
		if (direction == Direction.Bas   ) goal = (cf.grid.y  ) * cf.camSize.y - (box.bounds.extents.y + box.offset.y + 0.1F);
		if (direction == Direction.Gauche) goal = (cf.grid.x  ) * cf.camSize.x - (box.bounds.extents.x + box.offset.x + 0.1F);
		if (direction == Direction.Droite) goal = (cf.grid.x+1) * cf.camSize.x +  box.bounds.extents.x - box.offset.x + 0.1F;

		rbCible = cf.cible.GetComponent<Rigidbody2D>();
	}

	// apres les FixedUpdate, avant LateUpdate
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		SetCiblePos(cf.offset);
		SetCamPos(cf.offset);
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// placer la cible et la caméra AU POIL
		SetCiblePos(1);
		cf.SetGrid();
	}

	public void SetCiblePos(float t)
	{
		// suivant la direction, on lerp soit X, soit Y
		if ((int)direction < 2)
		{
			rbCible.MovePosition(
				Utils.ChangeY(
					cf.cible.position, 
					Mathf.Lerp(previous, goal, t)
				)
			);
		}
		else
		{
			rbCible.MovePosition(
				Utils.ChangeX(
					cf.cible.position, 
					Mathf.Lerp(previous, goal, t)
				)
			);
		}
	}
	public void SetCamPos(float t)
	{
		cf.transform.position = Vector2.Lerp(
			Vector2.Scale(cf.grid, cf.camSize),
			Vector2.Scale(cf.grid + Utils.DirToVec2((int)direction), cf.camSize), 
			t);
	}

}
