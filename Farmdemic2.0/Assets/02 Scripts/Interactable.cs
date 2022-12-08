#define PC
#define Moblie

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("State")]
    public bool isDrag;
    public bool isDragable;
    public bool isOnMouse;
    public bool isClick;

    [Header("Options")]
    // 드래그 중 손가락이 오브젝트에서 떨어져도 드래그가 유지되는 옴션
    public bool longRangeDrag;

    Collider coll;

    Vector3 prevPos;

    private void Awake()
    {
        coll = gameObject.GetComponent<Collider>();

        isDragable = true;
        isDrag = false;
        isOnMouse = false;
        isClick = false;
    }

    private void OnMouseDrag()
    {
        if (isDragable == false)
        {
            isDrag = false;
            return;
        }
        
        if (longRangeDrag == false && isOnMouse == false)
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
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            // 싱글 터치
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosToVector3 = new Vector3(touch.position.x,touch.position.y,100);

            if (touch.phase == TouchPhase.Moved)
            {
                isDrag = true;
                if(coll)
                {   
                    Ray ray = Camera.main.ScreenPointToRay(touchPosToVector3);
                    RaycastHit hit;                    

                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        Debug.DrawLine(ray.origin, hit.point, Color.red, 1.5f);
                        if(hit.collider.gameObject == gameObject)
                        {
                            Debug.Log("Touch begin");
                            isOnMouse = true;
                        }
                        else 
                        {
                            isOnMouse = false;
                        }
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                StopCoroutine(Click());
                StartCoroutine(Click());
            }
        }
    }

    IEnumerator Click()
    {
        isClick = true;
        yield return new WaitForEndOfFrame();
        isClick = false;
    }

}