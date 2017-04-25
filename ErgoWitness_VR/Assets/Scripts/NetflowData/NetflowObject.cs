﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Move this object from the source to the destination. The movement
/// begins after setting the destination. 
/// 
/// Control the color of this netflow data, which represents the type
/// of traffic that it is
/// 
/// Author: Ben Hoffman
/// </summary>
[RequireComponent(typeof(TrailRenderer))]
public class NetflowObject : MoveFromSourceToTarget
{
    #region Fields

    public ParticleSystem trailPartical;         // The emission module of this particle system

    private ParticleSystemRenderer headParticle; //The Particle system on this object

    private TrailRenderer _trailRend;

    #endregion


    #region Properties

    public Material HeadParticleMaterial
    {
        get
        {
            return headParticle.material;
        }

        set
        {
            headParticle.material = value;
        }
    }

    public Gradient TrailColor
    {
        set
        {
            // Set the tint color of the material
            _trailRend.colorGradient = value;
        }
    }

    #endregion


    /// <summary>
    /// Get a reference to the particles
    /// </summary>
    void Awake()
    {
        // Get the pulsing particles
        headParticle = GetComponent<ParticleSystemRenderer>();

        // Get the trail renderer componenet
        _trailRend = GetComponent<TrailRenderer>();
    }

    /// <summary>
    /// Sets the color of the trail particles to this gradient
    /// </summary>
    /// <param name="changeTo"></param>
    public void SetColor(Gradient changeTo)
    {
        // Get a reference to the main
        ParticleSystem.MainModule main = trailPartical.main;

        // Change the color to the gradient
        main.startColor = changeTo;
    }

}
