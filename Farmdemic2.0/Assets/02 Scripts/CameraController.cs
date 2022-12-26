using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    void Start()
    {
        if (Application.platform == RuntimePlatform.OSXEditor || 
        Application.platform == RuntimePlatform.OSXPlayer)
        {
            GetComponent<PostProcessVolume>().enabled = false;
        }   
    }
    void Update()
    {
        
    }

    public void ZoomEffect()
    {
        transform.DOMove(transform.forward * 5, 0.4f);
    }
}
