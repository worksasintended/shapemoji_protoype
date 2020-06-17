using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Canon Class with Event Methods
/// </summary>
/// <param name="aimDisabled">
/// Disables Drag & Drop Aim
/// </param>
public class Canon : MonoBehaviour
{
    public bool aimDisabled = false;

    private GameObject projectile;
    
    private GameObject harpoon;
    private bool onDrag = false;

    private float previousAngle = 0;
    private float nextAngle = 0;    

    /// <summary>
    /// Initializes private variables
    /// </summary>
    void Start()
    {
        projectile = GameObject.Find("HarpoonProjectile");
        harpoon = GameObject.Find("harpoon");       

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
        onDrag = false;
        aimDisabled = true;
        projectile.GetComponent<Projectile>().fired = true;
    }

    /// <summary>
    /// Update method is called once per Frame
    /// </summary>
    void Update()
    {
            
    }
}
