using UnityEngine;

[ExecuteInEditMode]
[RequireComponent (typeof (SpriteRenderer))]
// need SpriteCharles material

public class Teinte : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
    public Color teinte = new Color(1,1,1,0);

	void OnEnable() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		Update();
	}

	void OnDisable() {
		Reset();
	}

	void Update() {
		MaterialPropertyBlock mpb = new MaterialPropertyBlock();
		spriteRenderer.GetPropertyBlock(mpb);

		mpb.SetColor("_Teinte", teinte);

		spriteRenderer.SetPropertyBlock(mpb);
	}

	void Reset() {
		MaterialPropertyBlock mpb = new MaterialPropertyBlock();
		spriteRenderer.GetPropertyBlock(mpb);

		mpb.SetColor("_Teinte", Color.clear);

		spriteRenderer.SetPropertyBlock(mpb);
	}
}
