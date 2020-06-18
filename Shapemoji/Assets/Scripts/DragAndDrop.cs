using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    private bool isDragging;
    [SerializeField] private SpriteRenderer werkbank;
    [SerializeField] private SpriteRenderer emoji;
    private Vector2 inventoryPos;
    private Vector2 lastPos;

    void Awake()
    {
        inventoryPos = GetComponent<RectTransform>().anchoredPosition;
        lastPos = GetComponent<RectTransform>().anchoredPosition;
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
        RectTransform itemBorder = GetComponent<RectTransform>();
        RectTransform werkbankBorders = werkbank.GetComponent<RectTransform>();
        RectTransform emojiBorders = emoji.GetComponent<RectTransform>();
        
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);

        }else if(!rectOverlaps(itemBorder, werkbankBorders)){
            GetComponent<RectTransform>().anchoredPosition = inventoryPos;
        
        }else if(rectOverlaps(itemBorder, werkbankBorders))
        {
            GetComponent<RectTransform>().anchoredPosition = werkbank.GetComponent<RectTransform>().anchoredPosition;
            lastPos = GetComponent<RectTransform>().anchoredPosition;
        }else if (rectOverlaps(itemBorder, emojiBorders) && lastPos == werkbank.GetComponent<RectTransform>().anchoredPosition)
        {
            GetComponent<RectTransform>().anchoredPosition = emoji.GetComponent<RectTransform>().anchoredPosition;
        }
    }
    
    bool rectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
    {
        Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(rectTrans2.offsetMin.x, rectTrans2.offsetMin.y, rectTrans2.rect.width, rectTrans2.rect.height);

        return rect2.Overlaps(rect1);
    }
}