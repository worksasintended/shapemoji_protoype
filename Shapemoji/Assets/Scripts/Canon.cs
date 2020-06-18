using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Canon Class with Event Methods
/// </summary>
/// <param name="aimDisabled">
/// Disables Drag & Drop Aim
/// </param>
/// <param name="projectile">
/// Projectile linked to Canon
/// </param>
/// <param name="harpoon">
/// Harpoon linked to Canon
/// </param>
public class Canon : MonoBehaviour
{
    public bool aimDisabled = false;

    public GameObject projectile;
    
    public GameObject harpoon;
    private bool onDrag = false;

    private float previousAngle = 0;
    private float nextAngle = 0;    

    /// <summary>
    /// Start Method
    /// </summary>
    void Start()
    { 

    }

    /// <summary>
    /// Harpoon Aim using Drag & Drop
    /// </summary>
    void OnMouseDrag() {
        if(!aimDisabled) {
            Vector3 pos = transform.position;
            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            nextAngle = Vector3.SignedAngle(direction-pos, Vector3.up, Vector3.back);
            if(!onDrag) {
                previousAngle = nextAngle;
                onDrag = true;            
            } else {            
                harpoon.transform.Rotate(Vector3.forward,nextAngle-previousAngle);
                previousAngle = nextAngle;
            }
        }      
    }

    /// <summary>
    /// Sets projectile into fired state
    /// </summary>
    void OnMouseUp()
    {
        if(!aimDisabled) {
            onDrag = false;
            aimDisabled = true;
            projectile.GetComponent<Projectile>().fired = true;
        }
    }

    /// <summary>
    /// Update method is called once per Frame
    /// </summary>
    void Update()
    {
            
    }
}
