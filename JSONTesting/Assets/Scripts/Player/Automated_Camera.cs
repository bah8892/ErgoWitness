﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will move whatever object that is is on
/// in a sphereical pattern, zooming in on some of the 
/// new PC's and then slowyl zooming out
/// </summary>
public class Automated_Camera : MonoBehaviour {

    public static Automated_Camera currentAutoCam;
	public float zoomSpeed = 1f;
    [SerializeField]
    public float speed = 0.5f;       // How fast do we want to shoot this thing
    public MoveFromSourceToTarget targetpos;

    private GameObject centerOfWorld;
    private Vector3 newPos;
    public UnityEngine.UI.Slider slider;

    private bool isMobile;

    /// <summary>
    /// Set up the target position and look at it
    /// </summary>
    void Start ()
    {
        // Set the reference
        currentAutoCam = this;

        // Set the target position
        transform.LookAt(targetpos.transform.position);

        // Make a new game object at the center of the world
        centerOfWorld = new GameObject("centerOfWorld");
        centerOfWorld.transform.position = Vector3.zero;
        centerOfWorld.transform.rotation = Quaternion.identity;

        // If we are on a mobile platform
        if (Application.isMobilePlatform)
        {
            isMobile = true;
        }
    }

    /// <summary>
    /// Move the camera aroudn the target
    /// </summary>
    void Update()
    {
        // Rotate around the target position
        transform.RotateAround(targetpos.transform.position, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * speed);
        float scrollWheel = 0f;

        if (isMobile)
        {
            // If there are two touches on the device...
            if (Input.touchCount == 2)
            {
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                scrollWheel = prevTouchDeltaMag - touchDeltaMag;
            }
        }
        else
        {
            scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        }


        if (scrollWheel > 0f)
        {
            // Scroll in
            transform.Translate(Vector3.forward * Time.deltaTime * zoomSpeed);
            transform.LookAt(targetpos.transform.position);
        }
        else if (scrollWheel < 0f)
        {
            // Scroll out
            transform.Translate(-Vector3.forward * Time.deltaTime * zoomSpeed);
            transform.LookAt(targetpos.transform.position);
        }
        // Look at the target position
        transform.LookAt(targetpos.transform.position);

        if(slider.value != speed)
        {
            speed = slider.value;
        }

    }

    /// <summary>
    /// Set the new target position and look at it
    /// </summary>
    /// <param name="newTarget">The new target to set</param>
    public void ChangeTarget(Transform newTarget)
    {
        // Set the new target
        targetpos.SourcePos = newTarget;
        targetpos.DestinationPos = centerOfWorld.transform;
        // Look at the new target
        transform.LookAt(targetpos.transform.position);
        
    }

}
