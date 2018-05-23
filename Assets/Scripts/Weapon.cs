using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    private Vector2 direction;
    private Transform playerPosition;

    public int rotationOffset = 90;
    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;

    public int fireRate = 0;
    public float Damage = 10;
    public LayerMask whatToHit;

    public Transform bulletTrailPrefab;
    public Transform muzzleFlashPrefab;

    private float timeToFire = 0;
    private Transform firePoint;
    
	// Use this for initialization
	void Start() {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null) {
            Debug.LogError("OMG! NO FIREPOINT!!1");
        }
    }
	
	// Update is called once per frame
	void Update () {
        aim();
        if (fireRate == 0) {
            if (Input.GetButtonDown("Fire1")) {
                shoot();
            }
        }
        else {
            if (Input.GetButton("Fire1") && Time.time > timeToFire) {
                timeToFire = Time.time + 1 / fireRate;
                shoot();
            }
        }
	}

    //Following mouse script. Vector Calculations.
    void aim() {
        //subtraciting the position of the player form the mouse position
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        
        // normalize the vector. Meaning that all the sum of the vector will be equal to 1
        difference.Normalize();

        //find the angle in degrees
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //apply rotation to player
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
    }

    /**
     * Very nice shooting Machenic for multiple weapons.
     */
    void shoot() {
        Debug.Log("Shot!");
        //this way we translate mousepos from screen to game world.
        Vector2 mousePosition = new Vector2(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y
            );

        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        if (Time.time >= timeToSpawnEffect) {
            StartCoroutine(effect());
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100);

        if (hit.collider != null) {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + "and did " + Damage + " damage");
        }
    }

    //look for more details in coroutines!
    //Effect for the muzzleFlash system
    IEnumerator effect() {
        //to spawn smth. => Instantiate
        Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation);
        Transform muzzleFlashInstance = (Transform) Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);

        muzzleFlashInstance.parent = firePoint;
        float size = Random.Range(0.006f, 0.070f);
        muzzleFlashInstance.localScale = new Vector3(size, size, size);

        //Display it for one single frame and then destroy it.
        yield return 0;
        Destroy(muzzleFlashInstance.gameObject);
    }
}
