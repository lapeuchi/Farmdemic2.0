#define PC
#define Moblie

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    [Header("State")]
    public bool isDrag;
    public bool isDragable;

    Collider2D coll;

    Vector3 prevPos;


    private void Awake()
    {
        coll = gameObject.GetComponent<Collider2D>();

        isDragable = true;
        isDrag = false;

    }

    Vector3 screenSpace;
    Vector3 offset;
    private void OnMouseDown()
    {
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    }

    private void OnMouseDrag()
    {
        if (isDragable == false) return;

        isDrag = true;
        var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        isDrag = false;
    }

}