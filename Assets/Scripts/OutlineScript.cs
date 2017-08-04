using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class OutlineScript : MonoBehaviour {
    public Color color = Color.white;

    private SpriteRenderer spriteRenderer;

    void OnEnable() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateOutline(true);
    }

    void OnDisable() {
        UpdateOutline(false);
    }

    void Update() {
        UpdateOutline(true);
    }

    void UpdateOutline(bool outline) {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", outline ? 2f : 0);
        mpb.SetColor("_OutlineColor", color);
        spriteRenderer.SetPropertyBlock(mpb);
    }

	// Use this for initialization
	void Start () {
	
	}
}
