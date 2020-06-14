using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonCollisionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other){
        Debug.Log("Harpoon Event: Collider");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
