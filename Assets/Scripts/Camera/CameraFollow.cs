using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform cible;
	public Collider2D cibleVisibleBox;
	Camera cam;
	Animator animator;
	Collider2D cibleColl;
	Deplacements deplacementsCible;

	[HideInInspector]
	public Vector2 camSize;

	public Vector2 grid;
	public float offset;
	bool gameIsPlaying = true;

	TransitionState transitionState;
	[HideInInspector]
	public Vector3 placeToTeleport;


	void Start()
	{
		// components nécessaires
		cam               = GetComponentInChildren <Camera>       ();
		animator          = GetComponent           <Animator>     ();
		cibleColl         = cible.GetComponent     <Collider2D>   ();
		deplacementsCible = cible.GetComponent     <Deplacements> ();

		transitionState = animator.GetBehaviour <TransitionState> ();
		transitionState.cf = this;

		// raccourcis
		camSize.x = cam.orthographicSize * 2 * cam.aspect;
		camSize.y = cam.orthographicSize * 2;
		camSize = new Vector2(camSize.x, camSize.y);


		SetGrid();
	}

	public void SetGrid()
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

	void LateUpdate ()
	{
		// Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Transition"));
		if (gameIsPlaying)
		{
			Direction dirVoulue = (Direction)CibleVisibleBoxSortDeLecran();
			if ((int)dirVoulue > -1 // sort de l'écran
			&&  deplacementsCible.dir == dirVoulue
			&&  deplacementsCible.bouge)
			{
				transitionState.direction = dirVoulue;
				animator.SetTrigger("Transition");
			}
		}
	}

	int CibleVisibleBoxSortDeLecran()
	{
		// return values
		//	-1 : aucun depassement
		//	0,1,2,3 : depassement dans la Direction 0,1,2,3
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

		// test des LineCast sur tous les layers SAUF (~) celui du joueur et les safeZones
		int mask = ~((1 << LayerMask.NameToLayer("Player")) | ( 1 << LayerMask.NameToLayer("SafeZone")));

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
		if      (depassementVertical)   return (int)directionTransitionVertical;
		else if (depassementHorizontal) return (int)directionTransitionHorizontal;
		else return -1; // aucun depassement
	}


	void Pause()
	{
		gameIsPlaying = false;
		// disable cerveau et deplacements pour piloter l'anim tranquillement
		cible.GetComponent<Cerveau>().enabled = false;
		if (AnimationStateNameIs("Transition"))
		{
			deplacementsCible.enabled = false;
			cible.GetComponent<Animator>().SetFloat("vitesseLecture", 0.55F);
		}
		if (AnimationStateNameIs("FadeIn") || AnimationStateNameIs("FadeOut"))
		{
			deplacementsCible.bouge = true;
		}
	}

	void Play()
	{
		gameIsPlaying = true;

		// re-enable les controles
		cible.GetComponent<Cerveau>().enabled = true;
		deplacementsCible.enabled = true;
		cible.GetComponent<Animator>().SetFloat("vitesseLecture", 1F);
	}

	public void TeleportToPlace()
	{
		cible.position = placeToTeleport;
		deplacementsCible.bouge = false;
		SetGrid();
		Play();
	}

	bool AnimationStateNameIs(string s){return animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer."+s);}
}
