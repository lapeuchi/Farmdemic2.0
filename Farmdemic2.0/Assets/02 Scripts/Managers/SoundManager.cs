using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private Dictionary<Define.BGM, AudioClip> bgms = new Dictionary<Define.BGM, AudioClip>();
    private Dictionary<Define.SFX, AudioClip> sfxs = new Dictionary<Define.SFX, AudioClip>();
    private AudioSource bgmSource;
    private AudioSource[] sfxSource = new AudioSource[5];
    
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");

        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
        }

        Object.DontDestroyOnLoad(root);

        GameObject bgmRoot = new GameObject { name = "@BGM" };
        bgmSource = bgmRoot.AddComponent<AudioSource>();
        bgmRoot.transform.parent = root.transform;
        
        for(int i = 0; i < System.Enum.GetValues(typeof(Define.BGM)).Length; i++)
        {
            AudioClip clip = Managers.Resource.Load<AudioClip>($"Sounds/{(Define.BGM) i}");
            bgms.Add((Define.BGM) i, clip);
        }

        GameObject sfxRoot = new GameObject { name = "@SFX" }; ;
        sfxRoot.transform.parent = root.transform;

        for(int i = 0; i < sfxSource.Length; i++)
        {
            GameObject go = new GameObject { name = "SFX_Source" };
            go.transform.SetParent(sfxRoot.transform);
            sfxSource[i] = go.AddComponent<AudioSource>();
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(Define.BGM)).Length; i++)
        {
            AudioClip clip = Managers.Resource.Load<AudioClip>($"Sounds/{(Define.SFX) i}");
            sfxs.Add((Define.SFX)i, clip);
        }
    }

    public void PlayBGM(Define.BGM type)
    {
        bgmSource.clip = bgms[type];
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlaySFX(Define.SFX type, float pitch = 1f)
    {
        for(int i = 0; i < sfxSource.Length; i++)
        {
            if(sfxSource[i].isPlaying == false)
            {
                sfxSource[i].clip = sfxs[type];
                sfxSource[i].pitch = pitch;
                sfxSource[i].Play();
                return;
            }
        }
    }
}
