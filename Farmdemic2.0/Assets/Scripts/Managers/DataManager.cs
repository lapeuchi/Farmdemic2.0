using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Dictionary<Define.DataType, TextAsset> datas { get; private set; } = new Dictionary<Define.DataType, TextAsset>();
    
    public void Init()
    {

    }
}
