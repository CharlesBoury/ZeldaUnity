using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

		public static Vector3 RotateZ( this Vector3 v, float angle )
		{
			float sin = Mathf.Sin(angle);
			float cos = Mathf.Cos(angle);

			return new Vector3(
				(cos * v.x) - (sin * v.y),
				(cos * v.y) + (sin * v.x),
				v.z
				);
		}

}
