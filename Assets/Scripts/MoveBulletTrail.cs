using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBulletTrail : MonoBehaviour {

    public int moveSpeedOfBullet = 230;

	// Update is called once per frame
	void Update () {
        //moving objects over time without rigidbodys - just for one single direction. (transform.Translate())
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeedOfBullet);
        Destroy(this.gameObject, 0.50f);
	}
}
