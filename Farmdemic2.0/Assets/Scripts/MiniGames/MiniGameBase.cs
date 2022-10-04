using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGameBase : MonoBehaviour
{
    private Define.MiniGame _miniGame;
    public Define.MiniGame MiniGame { get {return _miniGame;} protected set {_miniGame = value;} }

    protected abstract void Init();

    protected virtual void GameOver(bool isClear)
    {
        MiniGameManager.instance.GameOver(isClear);
    }

}
