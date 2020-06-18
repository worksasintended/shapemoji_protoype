using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

///
/// <summary>
/// Class, which determines the Wheel Behaviour
/// </summary> 
/// <param name="projectile">
/// GameObject of the controlled harpoon projectile
/// </param>
/// <param name="revolutions">
/// number of revolutions to pull the rope
/// </param>
///
public class Wheel : MonoBehaviour
{
    public GameObject projectile;
    public float revolutions; 

    private bool onDrag = false;

    private float previousAngle = 0;
    private float nextAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 

    }

    private void OnMouseDrag()
    {
        Vector3 pos = transform.position;
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        nextAngle = Vector3.SignedAngle(direction - pos, Vector3.up, Vector3.back);
        if (!onDrag)
        {
            previousAngle = nextAngle;
            onDrag = true;
        }
        else
        {
            transform.Rotate(Vector3.forward, nextAngle - previousAngle);
            Projectile p = projectile.GetComponent<Projectile>();
            float distance = (nextAngle - previousAngle) / (360 * revolutions);
            p.addPullDistance(distance);
            previousAngle = nextAngle;
            
        }
    }

    private void OnMouseUp()
    {
        onDrag = false;
    }

}
