using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

	public static RaycastHit2D LinecastInDirection(
		Collider2D coll, 
		Vector2 extents, 
		Direction dir, 
		int mask, 
		bool showDebugLine = false, 
		Color debugLineColor = default(Color),
		float debugDisplayTime = 0F)
	{
		//     + dir.Haut(0)
		//     |
		//     |       lineStart
		//     |      /   lineEnd
		// +---+----+/   /
		// |  coll  +---+ dir.Droite(1) ...
		// +---+----+
		//     |      +---+
		//     |      |   |
		//     |      | extents
		//     +      |   |
		//            +---+

		Physics2D.queriesHitTriggers = false;

		Vector2 lineStart;
		if ((int)dir == 0)      lineStart = new Vector2 (coll.bounds.center.x, coll.bounds.max.y);
		else if ((int)dir == 1) lineStart = new Vector2 (coll.bounds.center.x, coll.bounds.min.y);
		else if ((int)dir == 2) lineStart = new Vector2 (coll.bounds.min.x, coll.bounds.center.y);
		else                    lineStart = new Vector2 (coll.bounds.max.x, coll.bounds.center.y);

		Vector2 lineEnd;
		if ((int)dir == 0)      lineEnd = lineStart + new Vector2 (0, extents.y);
		else if ((int)dir == 1) lineEnd = lineStart - new Vector2 (0, extents.y);
		else if ((int)dir == 2) lineEnd = lineStart - new Vector2 (extents.x, 0);
		else                    lineEnd = lineStart + new Vector2 (extents.x, 0);

		if (showDebugLine)
		{
			if (debugLineColor == default(Color)) debugLineColor = Color.green;
			Debug.DrawLine(lineStart, lineEnd, debugLineColor, debugDisplayTime);
		}
		return Physics2D.Linecast(lineStart, lineEnd, mask);
	}



	// VECTORS
	public static Vector3 ChangeX(Vector3 v, float n)           { return new Vector3(n, v.y, v.z); }
	public static Vector3 ChangeY(Vector3 v, float n)           { return new Vector3(v.x, n, v.z); }
	public static Vector3 ChangeZ(Vector3 v, float n)           { return new Vector3(v.x, v.y, n); }
	public static Vector3 ToVec3 (Vector2 v)                    { return new Vector3(v.x, v.y, 0); }
	public static Vector3 MultiplyVectors(Vector3 a, Vector3 b) { return new Vector3( a.x * b.x, a.y * b.y, a.z * b.z); }

	public static Vector2 DirToVec2(int i)
	{
		// Basé sur l'enum Direction
		if (i == 0) return Vector2.up;
		if (i == 1) return Vector2.down;
		if (i == 2) return Vector2.left;
		else        return Vector2.right;
	}
	public static Vector3 DirToVec3(int i)
	{
		if (i == 0) return Vector3.up;
		if (i == 1) return Vector3.down;
		if (i == 2) return Vector3.left;
		else        return Vector3.right;
	}

	public static Vector3 RotateZ( this Vector3 v, float angle )
	{
		float sin = Mathf.Sin(angle);
		float cos = Mathf.Cos(angle);

		return new Vector3(
			(cos * v.x) - (sin * v.y),
			(cos * v.y) + (sin * v.x),
			v.z);
	}
}
