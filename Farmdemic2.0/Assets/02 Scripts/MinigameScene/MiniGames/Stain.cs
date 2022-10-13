using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stain : MonoBehaviour
{
    Dragable Drag;
    SpriteRenderer spriteRenderer;

    float timer = 0;
    float time = 0.1f;
    [SerializeField] float hp;
    
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Drag = gameObject.GetComponent<Dragable>();
        hp = 100;
    }

    void Update()
    {
        if (Drag.isDrag)
        {
            timer += Time.deltaTime;
            if(timer > time)
            {
                hp -= 10;
                spriteRenderer.color = new Color (spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, hp / 100);
                timer = 0;
            }   
        }   
    }

}
