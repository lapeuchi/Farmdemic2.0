using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    public bool isDrag;
    public bool isDragable = true;
    public bool isOnMouse;
    public bool isLongRangeDrag;

    Collider coll;
    Collider2D coll2D;

    Vector3 prevPos;

    private void Start()
    {
        coll = gameObject.GetComponent<Collider>();
        coll2D = gameObject.GetComponent<Collider2D>();
        isDragable = true;
    }

    private void OnMouseDrag()
    {
        if (isDragable == false)
        {
            isDrag = false;
            return;
        }
         
        if (isLongRangeDrag == false && isOnMouse == false)
        {
            isDrag = false;
            return;
        } 

        if(prevPos != Input.mousePosition)
        {
            Debug.Log("Dragging");
            isDrag = true;
            prevPos = Input.mousePosition;
        }
        else
        {
            isDrag = false;
        }
    }

    // private void OnMouseEnter()
    // {
    //     isOnMouse = true;
    // }

    // void OnMouseExit()
    // {
    //     isOnMouse = false;
    // }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            // 싱글 터치
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos;
            Ray ray;
            RaycastHit hit;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Debug.Log("터치 시작");
                    Vector3 touchPosToVector3 = new Vector3(touch.position.x,touch.position.y,100);
                    touchPos = Camera.main.ScreenToWorldPoint(touchPosToVector3);
                    ray = Camera.main.ScreenPointToRay(touchPosToVector3);
                    GameObject childGo = GameObject.Find("Child");

                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        Debug.DrawLine(ray.origin, hit.point, Color.red, 1.5f);
                        if(hit.collider.tag == "Child")
                        {

                        }

                        else if(hit.collider.tag == "Parents")
                        {
                            Debug.Log("Parent 터치 !");
                        }
                    }
                    else
                    {
                        Debug.DrawLine(ray.origin,touchPos, Color.yellow, 1.5f);
                    }
                    break;

                case TouchPhase.Canceled:

                    break;
                
                case TouchPhase.Ended:

                    break;
                
                case TouchPhase.Moved:

                    break;
                
                case TouchPhase.Stationary:

                    break;
            }
        }
    }

    void BeginTouch()
    {
        
    }

    
}
