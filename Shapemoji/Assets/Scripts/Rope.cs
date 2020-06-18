using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rope class is used to stretch the Rope
/// </summary>
/// <param name="length">
/// Rope length
/// </param>
public class Rope : MonoBehaviour
{

    public float length;
    private float ropeUnit;
    private Vector3 initialScale;

    /// <summary>
    /// Set initial Rope length and save its initial values
    /// </summary>
    void Start()
    {
        length = 0;
        ropeUnit = GetComponent<Renderer>().bounds.size.x*0.65f;
        initialScale = transform.localScale;
    }

    /// <summary>
    /// Scale Rope according to length
    /// </summary>
    void Update()
    {
        float scaleValue;
        scaleValue = initialScale.x*(length + ropeUnit)/ropeUnit;
        Vector3 scale = new Vector3(scaleValue, initialScale.y , 0.0f);
        transform.localScale = scale;     
    }
}
