using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonDraggable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// moves the connected gameObject 
    /// </summary>
    /// <param name="speed"></param>
    public void moveDraggable(float speed)
    {
        transform.Translate(Vector3.down * speed);
    }

    public void rotate(float angle)
    {
        transform.Rotate(new Vector3(0,0,1),angle);
    }

    public void transferToInventory()
    {
        //TODO transfer to Inventory
        Destroy(gameObject);
    }
}
