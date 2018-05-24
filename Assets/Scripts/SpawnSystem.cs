using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour {

    public Vector2 center;
    public Vector2 size;

    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.9f;
    public Transform target;

    public GameObject enemyPrefab;
    public int waveCounts;

    private float timer;
    public float spawnRate;

    //Position of player.
    private Vector2 targetPos;


    // Use this for initialization
    void Start() {
        StartCoroutine(spawnEnemys());
    }

    // Update is called once per frame
    void FixedUpdate() {
        /* Following the player, so enemys spawn near of him. */
        this.targetPos = target.position;
        transform.position = Vector3.SmoothDamp(transform.position, this.targetPos, ref velocity, smoothTime);
    }

    IEnumerator spawnEnemys() {
        yield return new WaitForSeconds(3);
        while (true) {
            for (int i = 0; i < waveCounts; i++) {
                Vector2 position = this.targetPos + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
                Instantiate(enemyPrefab, position, Quaternion.identity);
                yield return new WaitForSeconds(spawnRate);
            }
        }
    }
}
