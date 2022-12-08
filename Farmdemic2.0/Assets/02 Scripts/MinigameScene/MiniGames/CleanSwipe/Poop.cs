using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    Interactable interactable;

    [SerializeField] Trash trash;
    [SerializeField] Stain stain;
    public bool isDie = false;

    void Start()
    {
        interactable = GetComponent<Interactable>();
        trash = trash.transform.Find("Trash").GetComponent<Trash>();
        stain = trash.transform.Find("Stain").GetComponent<Stain>();
    }

    void Update()
    {
        
    }

    IEnumerator f()
    {

        yield return new WaitUntil(()=>trash.isDie);
        
        yield return new WaitUntil(()=> stain.isDie);

        isDie = true;
    }
}
