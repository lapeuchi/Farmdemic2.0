using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public interface IMinigame
{
    ///<summary>
    // MinigameManager.GameStartEffect()에서 호출되는 게임 시작함수
    // 변수 초기화 등
    ///</summary>
    void GameStart();

    ///<summary>
    // MinigameManager.GameOver()에서 호출되는 게임 시작함수
    // 게임 클리어 계산 등 게임 결과 출력 준비과정 작성
    ///</summary>
    void GameOver();    
}