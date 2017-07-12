using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform cible;
	public Collider2D cibleVisibleBox;
	public Vector2 grid;
	public float offset;

	Animator animator;
	Camera cam;
	Vector2 camSize;

	Deplacements deplacementsCible;
	Rigidbody2D rbCible;
	Collider2D cibleColl;

	bool gameIsPlaying = true;
	Direction dirTransition;
	float previous;
	float goal;
	int mask;


	void Start()
	{
		// components nécessaires
		cam = GetComponentInChildren<Camera>();
		animator = GetComponent<Animator>();
		deplacementsCible = cible.GetComponent<Deplacements>();
		rbCible = cible.GetComponent<Rigidbody2D>();
		cibleColl = cible.GetComponent<Collider2D>();

		// raccourcis
		camSize.x = cam.orthographicSize * 2 * cam.aspect;
		camSize.y = cam.orthographicSize * 2;
		camSize = new Vector2(camSize.x, camSize.y);

		// test des LineCast sur tous les layers SAUF (~) celui du joueur et les safeZones
		mask = ~((1 << LayerMask.NameToLayer("Player")) | ( 1 << LayerMask.NameToLayer("SafeZone")));


		SetGrid();
	}

	void SetGrid()
	{
		if (cible != null)
		{
			grid.x = Mathf.FloorToInt(cible.position.x/camSize.x);
			grid.y = Mathf.FloorToInt(cible.position.y/camSize.y);
			grid = new Vector2(grid.x, grid.y);
		}
		// Set cam pos by grid
		transform.position = new Vector3(
			camSize.x * grid.x,
			camSize.y * grid.y,
			transform.position.z);
	}

	void FixedUpdate()
	{
		// if pause, set ciblePos
		if (!gameIsPlaying)
		{
			// suivant la direction, on lerp soit X, soit Y
			if ((int)dirTransition < 2)
			{
				rbCible.MovePosition(
					Utils.ChangeY(
						cible.position, 
						Mathf.Lerp(previous, goal, offset)
					)
				);
			}
			else
			{
				rbCible.MovePosition(
					Utils.ChangeX(
						cible.position, 
						Mathf.Lerp(previous, goal, offset)
					)
				);
			}
		}
	}

	void LateUpdate ()
	{
		animator.SetBool("VeutChangerDecran", 
			cibleVisibleBoxSortDeLecran()
		&&  deplacementsCible.dir == dirTransition
		&&  deplacementsCible.bouge);

		// set cam pos
		if (!gameIsPlaying) transform.position = Vector2.Lerp(
				Vector2.Scale(grid, camSize),
				Vector2.Scale(grid + Utils.DirToVec2((int)dirTransition), camSize), 
				offset);
	}

	bool cibleVisibleBoxSortDeLecran()
	{
		// on doit séparer horizontal et vertical, sinon quand on longe le haut de l'écran et 
		// que la box en sort, ça va pas trigger parce que le raycast vers le haut touche le bord du monde
		bool depassementVertical = false;
		bool depassementHorizontal = false;
		Direction directionTransitionVertical   = (Direction)0;
		Direction directionTransitionHorizontal = (Direction)0;

		if      (cibleVisibleBox.bounds.max.y > (grid.y+1) * camSize.y){ directionTransitionVertical   = Direction.Haut;   depassementVertical   = true; }
		else if (cibleVisibleBox.bounds.min.y < (grid.y  ) * camSize.y){ directionTransitionVertical   = Direction.Bas;    depassementVertical   = true; }
		if      (cibleVisibleBox.bounds.min.x < (grid.x  ) * camSize.x){ directionTransitionHorizontal = Direction.Gauche; depassementHorizontal = true; }
		else if (cibleVisibleBox.bounds.max.x > (grid.x+1) * camSize.x){ directionTransitionHorizontal = Direction.Droite; depassementHorizontal = true; }

		// on n'accepte les depassements que s'il n'y a rien devant
		if (depassementVertical) depassementVertical = Utils.LinecastInDirection(
			cibleColl,
			cibleVisibleBox.bounds.size,
			directionTransitionVertical,
			mask,
			true).collider == null;
		if (depassementHorizontal) depassementHorizontal = Utils.LinecastInDirection(
			cibleColl,
			cibleVisibleBox.bounds.size,
			directionTransitionHorizontal,
			mask,
			true).collider == null;

		// on ne retient qu'une direction, en preferant la verticale
		if      (depassementVertical)   dirTransition = directionTransitionVertical;
		else if (depassementHorizontal) dirTransition = directionTransitionHorizontal;
		else return false; // aucun depassement

		// si depassement confirmé, calcul du goal de la position de la cible
		if (dirTransition == Direction.Haut  ) goal = (grid.y+1) * camSize.y +  cibleVisibleBox.bounds.extents.y - cibleVisibleBox.offset.y + 0.1F;
		if (dirTransition == Direction.Bas   ) goal = (grid.y  ) * camSize.y - (cibleVisibleBox.bounds.extents.y + cibleVisibleBox.offset.y + 0.1F);
		if (dirTransition == Direction.Gauche) goal = (grid.x  ) * camSize.x - (cibleVisibleBox.bounds.extents.x + cibleVisibleBox.offset.x + 0.1F);
		if (dirTransition == Direction.Droite) goal = (grid.x+1) * camSize.x +  cibleVisibleBox.bounds.extents.x - cibleVisibleBox.offset.x + 0.1F;

		return true;
	}

	void Pause()
	{
		gameIsPlaying = false;
		// save previousPos de la cible pour le bouger relativement
		if ((int)dirTransition < 2) previous = cible.position.y;
		else                        previous = cible.position.x;

		// disable cerveau et deplacements pour piloter l'anim tranquillement
		cible.GetComponent<Cerveau>().enabled = false;
		deplacementsCible.enabled = false;
		cible.GetComponent<Animator>().SetFloat("vitesseLecture", 0.6F);
	}

	void Play()
	{
		// placer le joueur AU POIL
		if ((int)dirTransition < 2) rbCible.MovePosition( Utils.ChangeY(cible.position, goal) );
		else                        rbCible.MovePosition( Utils.ChangeX(cible.position, goal) );

		// placer la cam AU POIL
		SetGrid();
		gameIsPlaying = true;
		// re-enable les controles
		cible.GetComponent<Cerveau>().enabled = true;
		deplacementsCible.enabled = true;
		cible.GetComponent<Animator>().SetFloat("vitesseLecture", 1F);
	}
}
