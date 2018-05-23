using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour {

    public Vector2 offset = new Vector2(-3, -3);

    private SpriteRenderer spriteRendCaster;
    private SpriteRenderer spriteRendShadow;

    private Transform transCaster;
    private Transform transSahdow;

    public Material shadowMaterial;
    public Color shadowColor;

	// Use this for initialization
	void Start () {
        transCaster = transform;
        transSahdow = new GameObject().transform;
        transSahdow.parent = transCaster;
        transSahdow.gameObject.name = "shadow";
        transSahdow.localRotation = Quaternion.identity;

        spriteRendCaster = GetComponent<SpriteRenderer>();
        spriteRendShadow = transSahdow.gameObject.AddComponent<SpriteRenderer>();

        spriteRendShadow.material = shadowMaterial;
        spriteRendShadow.color = shadowColor;
        spriteRendShadow.sortingLayerName = spriteRendCaster.sortingLayerName;
        spriteRendShadow.sortingOrder = spriteRendCaster.sortingOrder - 1;
    }
	
	// LateUpdate is called once per frame - after update
	void LateUpdate () {
        transSahdow.position = new Vector2(transCaster.position.x + offset.x,
            transCaster.position.y + offset.y);

        spriteRendShadow.sprite = spriteRendCaster.sprite;
	}
}
