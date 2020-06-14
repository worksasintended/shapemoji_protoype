using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonCollisionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
/*
    void OnCollisionEnter(Collision collision) {
        Debug.Log("Harpoon Event: Collider");
    }

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Harpoon Event: Collider");

    }
*/
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Harpoon Event: Collider");        
        GameObject.Find("HarpoonCanon").GetComponent<HarpoonFireScript>().fired = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
