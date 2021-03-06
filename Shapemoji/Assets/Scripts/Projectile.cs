﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Projectile Class with Event Handlers as Methods
/// </summary>
/// <param name="fired">
/// Set true in order to fire the Projectile
/// </param>
/// <param name="speed">
/// Speed of the fired Projectile
/// </param>
/// <param name="rope">
/// Rope linked to Projectile
/// </param>
/// <param name="harpoonZRotation">
/// configurable angle to turn the collided stone by
/// </param>
/// <param name="maxFireDistance">
/// defines the maximum allowed fire distance
/// </param>
public class Projectile : MonoBehaviour
{
    public bool fired = false;
    public float speed = 400;
    public GameObject rope;
    public float harpoonZRotation = 45;
    public float maxFireDistance = 1500;

    private Vector3 initial;

    private GameObject draggable;
    private bool obtain = false;
    private float maxDistance = 0;
    private float pullDistance = 0; // Current allowed distance to pull

    /// <summary>
    /// Method to set initial projectile position
    /// </summary>
    void Start()
    {
        initial = transform.position;
    }

    /// <summary>
    /// Stop movement on collision
    /// </summary>
    void OnTriggerEnter2D(Collider2D other) {       
        if (other != null)
        {
            if (!obtain && fired)
            {
                fired = false;
                obtain = true;
                maxDistance = getDistanceToCanon();
                if (other != null) draggable = other.gameObject;
                if (draggable != null)
                {
                    if (draggable.GetComponent<HarpoonDraggable>() != null)
                    {
                        float angle = GameObject.Find("harpoon").transform.rotation.eulerAngles.z;
                        draggable.GetComponent<HarpoonDraggable>().rotate(harpoonZRotation + angle);
                    }
                    else
                    {
                        draggable = null;
                    }


                }

            }
            else
            {
                obtain = false;
                GameObject.Find("HarpoonCanon").GetComponent<Canon>().aimDisabled = false;
                maxDistance = 0;
                pullDistance = 0;

                if (draggable != null)
                {
                    draggable.GetComponent<HarpoonDraggable>().transferToInventory();
                }

                draggable = null;
            }
        }

    }

    /// <summary>
    /// Updates projectile coordinates
    /// </summary>
    void Update()
    {
        float travelDistance = speed * Time.deltaTime;
        if (fired) {
            if (maxFireDistance - getDistanceToCanon() < travelDistance) travelDistance = maxFireDistance - getDistanceToCanon();
            if (travelDistance<=1)
            {
                OnTriggerEnter2D(null); //handle stop of harpoon as if an item has been hit
            }
            transform.Translate(Vector3.right * travelDistance);
            rope.GetComponent<Rope>().length = (transform.position-initial).magnitude;
        } 
        else if (obtain)
        {
            if (travelDistance > pullDistance) travelDistance = pullDistance;
            pullDistance -= travelDistance;

            transform.Translate(travelDistance * (-1 * Vector3.right));
            float distPull = getDistanceToCanon(); ; //calculate remaining distance 
            rope.GetComponent<Rope>().length = distPull;

            if (draggable != null) draggable.GetComponent<HarpoonDraggable>().moveDraggable(travelDistance);
        }

    }

    /// <summary>
    /// get the current distance of the projectile from the canon
    /// </summary>
    /// <returns></returns>
    private float getDistanceToCanon()
    {
        return (transform.position - initial).magnitude;
    }

    /// <summary>
    /// determine by how much the projectile should be pulled in
    /// </summary>
    /// <param name="percentage"></param>
    public void addPullDistance(float percentage)
    {
        if (obtain)
        {
            pullDistance += percentage * maxDistance;
            if (pullDistance < 0) pullDistance = 0;
        }
        
    }
}
