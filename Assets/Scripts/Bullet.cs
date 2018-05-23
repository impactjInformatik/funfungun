using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Sprite defaultSprite;
    public Sprite muzzleFlash;

    public int framesToFlash = 3;
    public float destroyTime = 3;

    private Vector2 target;
    public float speed;

    private SpriteRenderer spriteRend;

	// Use this for initialization
	void Start () {
        spriteRend = GetComponent<SpriteRenderer>();
        defaultSprite = spriteRend.sprite;

        StartCoroutine(FlashMuzzleFlash());
        StartCoroutine(TimedDestruction());
	}

    IEnumerator FlashMuzzleFlash() {
        spriteRend.sprite = muzzleFlash;
        for (int i = 0; i < framesToFlash; i++) {
            yield return 0;
        }
    spriteRend.sprite = defaultSprite;
    }

    IEnumerator TimedDestruction() {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
