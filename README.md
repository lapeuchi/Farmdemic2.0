# [VSH Studio]
> 경북소프트웨어고등학교 2022년 2학기 캡스톤 팀 VSH Studio의 Farmdemic2.0 프로젝트

이 프로젝트는 [Farmdemic2.0](https://github.com/lapeuchi/Farmdemic2.0) 프로젝트로 마이그레이션되어 있습니다.

이 프로젝트의 진행정보는 [VSH팀 X 인플랩 게임사업팀](https://www.notion.so/gakain-game-develop/VSH-X-f96438900e77488aa343016746a96fc3) 페이지에서 확인하실 수 있습니다.


## Team Information
1. 김환희 - 프로그래머
2. 김지관 - 프로젝트 매니저
3. 최원준 - 프로그래머 (팀장)
4. 한용균 - 프로그래머
5. 홍대연 - 아티스트



## Overview

### 캡스톤 참여 동기
사업체에 멘토링을 받으면서 우리의 역량과 노하우를 늘리고 실무에 더 가까워 질 수 있을 것이라고 기대하여 
이번 캡스톤에 참여하게 되었습니다.

### 프로젝트 실행 동기
가축 질병으로 인한 재해는 한번의 발생으로 매우 커다란 피해를 

### 프로젝트 장르

### 프로젝트 목적

### 프로젝트 상세 설명(시놉시스, 주요 상호작용, 기타등등)

Some of the files in this archived GitHub project have the same Guid as files in
the new GitHub project. This allows projects that are using Play assets, such as
the `LoadingScreen` MonoBehaviour, to still work after migrating.

The new **Google Play Plugins for Unity** project uses the namespace
`Google.Play.Instant` instead of `GooglePlayInstant`, so any `using
GooglePlayInstant;` statements will have to be updated.

The new **Google Play Plugins for Unity** no longer supports the ability to set a custom instant apps URL.

## Migration steps

1.  Familiarize yourself with the
    process for the new plugin, and either download the new `.unitypackage` file
    or set up the **Game Package Registry for Unity**.

1.  Delete the existing `Assets/GooglePlayInstant` directory (if you previously
    imported from a `.unitypackage` file) or the
    `Assets/play-instant-unity-plugin` directory (if you previously imported via
    `git clone`). Note that at this point there may be errors such as "error
    CS0103: The name `InstallLauncher' does not exist in the current context" in
    the project.

1.  Import the `.unitypackage` obtained from the first step or install the
    **Google Play Instant** package in Unity Package Manager.

1.  Change any `using GooglePlayInstant;` statements to `using
    Google.Play.Instant;`.

## Known issues

### Launching an instant app from the Play Store redirects to the browser

This issue occurs when launching an instant app if the following is true:
1. Your currently released instant app is launchable via a custom URL.
1. You have uploaded a version of your instant app that does not specify a custom URL to alpha or internal test.
1. You are trying to launch this non-production version of your instant app.

The latest plugin no longer supports the ability to set a custom instant apps URL. If your app previously included a custom instant apps URL, uploading an app built with the latest plugin could trigger this issue.

There are two workarounds:
1. The issue will not occur for an instant app in production, so release the app to production to eliminate the issue.
1. If you'd prefer to fix the issue in alpha, add the browsable intent filter and default url tags to the UnityPlayerActivity in your app's manifest:
