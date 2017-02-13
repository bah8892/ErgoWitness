﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is going to be like the computer script, but for netflow
/// data. This object will be pooled, like bullets in the Unity
/// example. So I need a script that will have it be activated/
/// deactivated on enable and disable
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class NetflowObject : MonoBehaviour {

    // The different colors for the protocols
    public Color tcpColor;
    public Color udpColor;
    public Color httpColor;
    public Color httpsColor;

    public float smoothing = 10f;       // How fast do we want to shoot this thing

    private Transform source;
    private Transform destinaton;

    private LineRenderer lineRend;

    /// <summary>
    /// On set this changes the current position to source position
    /// </summary>
    public Transform Source
    {
        get { return source; }
        set
        {
            source = value;
            transform.position = source.position;
        }
    }
    /// <summary>
    /// On set this will start to move this object with a coroutine so that nothing else needs to happen
    /// </summary>
    public Transform Destination
    {
        get { return destinaton; }
        set
        {
            destinaton = value;

            // Stop the coroutine and start it again with a new destination
            StopCoroutine("MoveToDestination");
            StartCoroutine("MoveToDestination");

        }
    }

    // Use this for initialization
    void Start ()
    {
        lineRend = GetComponent<LineRenderer>();	
	}

    /// <summary>
    /// This is how I am gonna move between the source and destion
    /// </summary>
    /// <returns></returns>
	public IEnumerator MoveToDestination()
    {
        while (Vector3.Distance(transform.position, destinaton.position) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, destinaton.position, smoothing * Time.deltaTime);

            yield return null;
        }
    }


}
