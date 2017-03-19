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
    private float speed = 0.5f;       // How fast do we want to shoot this thing
    public MoveFromSourceToTarget targetpos;

    private GameObject centerOfWorld;
    private Vector3 newPos;
    public UnityEngine.UI.Slider slider;


    /// <summary>
    /// Set up the target position and look at it
    /// </summary>
    void Start ()
    {
        // Set the reference
        currentAutoCam = this;
        // Set the target position
        //targetpos = Vector3.zero;
        transform.LookAt(targetpos.transform.position);

        // Make a new game object at the center of the world
        centerOfWorld = new GameObject("centerOfWorld");
        centerOfWorld.transform.position = Vector3.zero;
        centerOfWorld.transform.rotation = Quaternion.identity;
    }

    /// <summary>
    /// Move the camera aroudn the target
    /// </summary>
    void Update()
    {
        //transform.LookAt(targetpos);
        //transform.Translate(Vector3.right * Time.deltaTime * speed);
        transform.RotateAround(targetpos.transform.position, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * speed);

        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
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
        transform.LookAt(targetpos.transform.position);

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


    /// <summary>
    /// Moves backwards the given amount with transform.translate
    /// </summary>
    /// <returns>The backwards.</returns>
    /// <param name="amount">Amount.</param>
    public IEnumerator MoveBackwards(float amount)
	{
		transform.Translate (-Vector3.forward * Time.deltaTime * zoomSpeed * amount);
        transform.LookAt(targetpos.transform.position);

        yield return null;
	}

    //Change the speed based on slider value
    public void SubmitSliderSettings()
    {
        speed = slider.value;
    }
}
