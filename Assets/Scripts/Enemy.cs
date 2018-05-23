using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    private Transform playerPos;
    private PlayerMovement player;

    private Transform enemy;
    public int rotationOffset = 90;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        lookAtPlayer();
	}

    /* Always following player */
    private void lookAtPlayer() {
        Vector3 difference = playerPos.position - transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            player.health--;
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Bullet")) {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
