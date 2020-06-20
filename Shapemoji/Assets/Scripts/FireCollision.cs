using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireCollision : MonoBehaviour
{
    private bool isDragging;
    

    public void OnMouseDown(){
        isDragging = true;
    }

    public void OnMouseUp(){
        isDragging = false;
    }

    void Update(){
        if(isDragging){
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D fireObject){
        fireObject.gameObject.SetActive(false);
    }

    
}
