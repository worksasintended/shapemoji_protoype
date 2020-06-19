using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    private bool isDragging;
    [SerializeField] private SpriteRenderer workstation;
    [SerializeField] private SpriteRenderer emoji;
    private Vector2 inventoryPos;
    private Vector2 lastPos;

    void Awake()
    {
        inventoryPos = GetComponent<RectTransform>().anchoredPosition;
        lastPos = transform.position;
    }
    
    public void OnMouseDown()
    {
        isDragging = true;
    }

    public void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        RectTransform stoneBorder = GetComponent<RectTransform>();
        RectTransform workstationBorders = workstation.GetComponent<RectTransform>();
        RectTransform emojiBorders = emoji.GetComponent<RectTransform>();

        Rect rectWorkstation = new Rect(getLeftWorldCorner(workstationBorders).x, getLeftWorldCorner(workstationBorders).y, workstationBorders.rect.width, workstationBorders.rect.height);
        Vector2 workstationCenter = new Vector2(rectWorkstation.center.x+220, rectWorkstation.center.y+220);
        Rect rectEmoji = new Rect(-1610, -434, 800, 800);
        
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
            
        }else if(rectOverlaps(stoneBorder, rectWorkstation))
        {
            transform.position = workstationCenter;
            lastPos = workstationCenter;

        }else if (rectOverlaps(stoneBorder, rectEmoji) && lastPos==workstationCenter)
        {
            Debug.Log(lastPos);
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = inventoryPos;
            lastPos = transform.position;
        }
    }
    
    bool rectOverlaps(RectTransform rectTrans1, Rect rect2)
    {
        Rect rect1 = new Rect(getLeftWorldCorner(rectTrans1).x, getLeftWorldCorner(rectTrans1).y, rectTrans1.rect.width, rectTrans1.rect.height);
        
        if (rect1.center.x > rect2.xMin+100 && rect1.center.x < rect2.xMax+150)
        {
            if (rect1.center.y > rect2.yMin+100 && rect1.center.y < rect2.yMax+300)
            {
                return true;
            }
        }
        return false;
    
    }
    Vector2 getLeftWorldCorner(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        return v[0];
    }
}