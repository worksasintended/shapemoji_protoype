using System.Collections;
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
public class Projectile : MonoBehaviour
{
    public bool fired = false;
    public float speed = 400;
    public GameObject rope;
    private Vector3 initial;

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
        if (!obtain && fired) { 
            fired = false;
            obtain = true;
            maxDistance = getDistanceToCanon();
        } else
        {
            obtain = false;
            GameObject.Find("HarpoonCanon").GetComponent<Canon>().aimDisabled = false;
        }
    }

    /// <summary>
    /// Updates projectile coordinates
    /// </summary>
    void Update()
    {
        if(fired) {
            transform.Translate(speed * Vector3.right * Time.deltaTime);
            rope.GetComponent<Rope>().length = (transform.position-initial).magnitude;
        }

        if (obtain)
        {
            float lenPull = speed;
            
            transform.Translate(lenPull * (-1 * Vector3.right) * Time.deltaTime);
            float distPull = getDistanceToCanon(); ; //calculate remaining distance 
            rope.GetComponent<Rope>().length = distPull;

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
}
