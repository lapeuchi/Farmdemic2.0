using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Slider progressBar;
    [SerializeField] TMP_Text percent_text;

    private void Start()
    {
        progressBar = GameObject.Find("Slider").GetComponent<Slider>();
        percent_text = GameObject.Find("Percent_Text").GetComponent<TMP_Text>();
        progressBar.value = 0;
        percent_text.text = "0%";
        StartCoroutine(LoadScene());
        Managers.Sound.StopBGM();
        Managers.Sound.StopAllSfx();
    }
    
    public static void LoadScene(string scene)
    {
        nextScene = scene;
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
    
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.value = Mathf.Lerp(progressBar.value, op.progress * 10, timer);
                percent_text.text = $"{(int)(op.progress*100)}%";
                if (progressBar.value >= op.progress*10)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.value = Mathf.Lerp(progressBar.value, 10f, timer);
                percent_text.text = $"{(int)(op.progress*100)}%";
                if (progressBar.value == 10f)
                {
                    percent_text.text = $"{100}%";
                    op.allowSceneActivation = true;
                    Managers.Sound.StopBGM();
                    yield break;
                }
            }
        }
    }
}