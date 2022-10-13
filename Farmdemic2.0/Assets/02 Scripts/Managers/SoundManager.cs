using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private Dictionary<Define.BGM, AudioClip> bgms = new Dictionary<Define.BGM, AudioClip>();
    private Dictionary<Define.SFX, AudioClip> sfxs = new Dictionary<Define.SFX, AudioClip>();
    private GameObject root;
    private AudioSource bgmSoruce;
    public void Init()
    {
        root = GameObject.Find("@Sound");

        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
        }
        
        root.transform.parent = GameObject.Find("@Managers").transform;
        GameObject bgmRoot = root.transform.Find("@BGM").gameObject;

        if(bgmRoot == null)
        {
            bgmRoot = new GameObject { name = "@BGM" };
            bgmRoot.AddComponent<AudioSource>();
        }

        bgmSoruce = bgmRoot.GetComponent<AudioSource>();

        for(int i = 0; i < System.Enum.GetValues(typeof(Define.BGM)).Length; i++)
        {
            AudioClip clip = Managers.Resource.Load<AudioClip>($"Sound/{(Define.BGM) i}");
            bgms.Add((Define.BGM) i, clip);
        }

        GameObject sfxRoot = root.transform.Find("@SFX").gameObject;

        if(sfxRoot == null)
        {
            sfxRoot = new GameObject { name = "@SFX" };
        }
    }
}
