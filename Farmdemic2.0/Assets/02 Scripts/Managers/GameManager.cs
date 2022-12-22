using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public Define.Story CurrentStory = Define.Story.Intro;

    public void StoryClear()
    {
        string[] capterArray = Enum.GetNames(typeof(Define.Story));
    }
}
