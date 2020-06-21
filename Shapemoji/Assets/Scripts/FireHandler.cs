using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHandler : MonoBehaviour
{
private bool allDestroyed = false;

    public GameObject fp1, fp2, fp3, fp4, fp5, fp6, fp7, fp8, clear;

    public float spawnRate= 1f;
    float nextSpawn = 0f;
    int whichSpwan; 

    void Start(){
        clear.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(!allDestroyed){
            if(Time.time > nextSpawn){
                whichSpwan = Random.Range(1,9);
                switch(whichSpwan){
                    case 1:
                        fp1.SetActive(true);
                        break;  
                    case 2:
                        fp2.SetActive(true);
                        break;  
                    case 3:
                        fp3.SetActive(true);
                        break;   
                    case 4:
                        fp4.SetActive(true);
                        break;    
                    case 5:
                        fp5.SetActive(true);
                        break;   
                    case 6:
                        fp6.SetActive(true);
                        break;   
                    case 7:
                        fp7.SetActive(true);
                        break;    
                    case 8:
                        fp8.SetActive(true);
                        break;                   
                }
                nextSpawn = Time.time + spawnRate;
            }
            allDestroyed = !(fp1.activeSelf || fp2.activeSelf || fp3.activeSelf || fp4.activeSelf || fp5.activeSelf || fp6.activeSelf || fp7.activeSelf || fp8.activeSelf );
        }else{
            clear.SetActive(true);
        }
    }
}
