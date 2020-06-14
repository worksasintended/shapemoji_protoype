using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotationScript : MonoBehaviour
{

    private GameObject wheel;
    private GameObject harpoon;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("WheelRotationScript Start method");
        wheel = GameObject.Find("Wheel");
        harpoon = GameObject.Find("harpoon");
    }

    void OnMouseDown()
    {
        wheel.transform.Rotate(Vector3.forward,45.0f);
        harpoon.transform.Rotate(Vector3.forward,5.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
