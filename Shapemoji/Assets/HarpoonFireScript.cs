using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonFireScript : MonoBehaviour
{
    private GameObject projectile;
    private GameObject rope;
    public int speed = 200;
    public bool fired = false;
    private Vector3 initial;
    private GameObject harpoon;

    private float previousAngle = 0;
    private float nextAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        projectile = GameObject.Find("HarpoonProjectile");
        rope = GameObject.Find("Rope");
        harpoon = GameObject.Find("harpoon");
        initial = projectile.transform.position;

    }
/*
    void OnMouseDrag() {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        nextAngle = Vector3.Angle(direction, Vector3.down);
        harpoon.transform.Rotate(Vector3.forward,nextAngle-previousAngle);
        previousAngle = nextAngle;
    }
*/
    void OnMouseDown()
    {
        Debug.Log("Harpoon Event: OnMouseDown");
        fired = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(fired) {
            Vector3 scale = new Vector3(6.5f * (projectile.transform.position-initial).magnitude/initial.magnitude, 1.0f, 0.0f);
            projectile.transform.Translate(speed * Vector3.right * Time.deltaTime);
            rope.transform.localScale = scale;
        }        
    }
}
