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
    public int enemyCount;

    private float timer;
    public float spawnRate;

    // Use this for initialization
    void Start() {
        StartCoroutine(spawnEnemys());
    }

    // Update is called once per frame
    void Update() {
        Vector2 targetPos = target.position;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        
    }

    IEnumerator spawnEnemys() {
        Vector2 targetPos = target.position;
        Vector2 position = targetPos + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
        Instantiate(enemyPrefab, position, Quaternion.identity);
        yield return new WaitForSeconds(spawnRate);
    }
}
