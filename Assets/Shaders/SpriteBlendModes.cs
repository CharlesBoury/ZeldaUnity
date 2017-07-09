using UnityEngine;

[ExecuteInEditMode]
[RequireComponent (typeof (SpriteRenderer))]
// need SpriteBlendModes.mat

public class SpriteBlendModes : MonoBehaviour {

	public enum BlendModes {Normal, Darken, Lighten, Multiply, Screen, Overlay, HardLight, SoftLight};

	private SpriteRenderer spriteRenderer;
    public BlendModes blendMode = BlendModes.Normal;

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

		mpb.SetFloat("_BlendMode", (float)blendMode);

		spriteRenderer.SetPropertyBlock(mpb);
	}

	void Reset() {
		MaterialPropertyBlock mpb = new MaterialPropertyBlock();
		spriteRenderer.GetPropertyBlock(mpb);

		mpb.SetFloat("_BlendMode", 0);

		spriteRenderer.SetPropertyBlock(mpb);
	}
}
