using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigameBase : MonoBehaviour
{
    private Define.Minigame _miniGame;
    public Define.Minigame MiniGame { get {return _miniGame;} protected set {_miniGame = value;} }

    protected abstract void Init();

    private void OnEnable() 
    {
        Init();
    }
    
    protected virtual void GameOver(bool isClear)
    {
        MinigameManager.instance.GameOver(isClear);
    }

}
