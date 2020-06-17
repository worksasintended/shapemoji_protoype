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

    /// <summary>
    /// Set initial Rope length
    /// </summary>
    void Start()
    {
        length = transform.localScale.x;
    }

    /// <summary>
    /// Scale Rope according to length
    /// </summary>
    void Update()
    {
        Vector3 scale = new Vector3(length*1.0f, 100.0f, 0.0f);
        transform.localScale = scale;
        
    }
}
