using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stain : MonoBehaviour
{
    Interactable Drag;
    SpriteRenderer spriteRenderer;

    float timer = 0;
    float time = 0.1f;

    [SerializeField] float hp;
    public float HP { get {return hp;} set {hp = value;} }

    public bool isDie = false;
    
    void Start()
    {
        isDie = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Drag = gameObject.GetComponent<Interactable>();
        if(Drag == null) Drag = gameObject.AddComponent<Interactable>();
    }

    void Update()
    {
        if (Drag.isDrag && Drag.isOnMouse)
        {
            timer += Time.deltaTime;
            if(timer > time)
            {
                hp -= 10;
                // spriteRenderer.color = new Color (spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, hp / 100);
                timer = 0;
            } 
            if(hp <= 0)
            {
                isDie = true;
                
            }  
        }
    }

}
