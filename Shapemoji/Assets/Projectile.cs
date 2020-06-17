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
public class Projectile : MonoBehaviour
{
    public bool fired = false;
    public float speed = 400;
    private Vector3 initial;

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
        fired = false;
    }

    /// <summary>
    /// Updates projectile coordinates
    /// </summary>
    void Update()
    {
        if(fired) {
            transform.Translate(speed * Vector3.right * Time.deltaTime);
            GameObject.Find("Rope").GetComponent<Rope>().length = 6.5f * (transform.position-initial).magnitude/initial.magnitude;

        }
    }
}
