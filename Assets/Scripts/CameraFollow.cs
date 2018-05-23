using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    //what we are following
    public Transform target;
    private Vector3 velocity = Vector3.zero;

    //time to follow target
    public float smoothTime = 0.90f;

    //enable and set the max y value
    public bool yMaxEnabled = false;
    public float yMaxValue = 0;

    //enable and set the min y value
    public bool yMinEnabled = false;
    public float yMinValue = 0;

    //enable and set the max x value
    public bool xMaxEnabled = false;
    public float xMaxValue = 0;

    //enable and set the min x value
    public bool xMinEnabled = false;
    public float xMinValue = 0;


    private void FixedUpdate() {
        //target position
        Vector3 targetPos = target.position;

        //vertical
        if (yMinEnabled && yMaxEnabled) {
            targetPos.y = Mathf.Clamp(target.position.y, yMinValue, yMaxValue);
        }
        else if (yMinEnabled) {
            targetPos.y = Mathf.Clamp(target.position.y, yMinValue, targetPos.y);
        }
        else if (yMaxEnabled) {
            targetPos.y = Mathf.Clamp(target.position.y, target.position.y, yMaxValue);
        }

        //Horizontal
        if (xMinEnabled && xMaxEnabled) {
            targetPos.x = Mathf.Clamp(target.position.x, xMinValue, xMaxValue);
        }
        else if (xMinEnabled) {
            targetPos.x = Mathf.Clamp(target.position.x, xMinValue, targetPos.x);
        }
        else if (xMaxEnabled) {
            targetPos.x = Mathf.Clamp(target.position.x, target.position.x, xMaxValue);
        }

        //align the camera and the target z position
        targetPos.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
