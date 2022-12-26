using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DisinfectantCardController : MonoBehaviour
{
    private Vector3 originPoint;
    private Vector3 readyPoint;
    private Vector3 fadeOutPoint;

    private Transform disinfectantCard;
    private Transform nextDisinfectantCard;

    private RectTransform currentCard;
    private RectTransform nextCard;

    private TMP_Text disinfectantText;
    private Image nextDisinfectantImage;
    private Image currentDisinfectantImage;

    private Color originColor;
    private Vector2 originSize;

    private string next;

    public void Init()
    {
        disinfectantCard = transform.Find("CurrentDisinfectantCard");
        currentDisinfectantImage = disinfectantCard.GetComponent<Image>();
        currentCard = disinfectantCard.GetComponent<RectTransform>();
        disinfectantText = disinfectantCard.GetComponentInChildren<TMP_Text>();
        originColor = currentDisinfectantImage.color;
        originSize = currentCard.sizeDelta;

        nextDisinfectantCard = transform.Find("NextDisinfectantCard");
        nextDisinfectantImage = nextDisinfectantCard.GetComponent<Image>();
        nextCard = nextDisinfectantCard.GetComponent<RectTransform>();

        readyPoint = transform.Find("ReadyPoint").position;
        fadeOutPoint = transform.Find("FadeOutPoint").position;
        originPoint = disinfectantCard.position;
    }

    public void Switch(string text)
    {
        next = text;
        StartCoroutine(FadeAnimation());
    }

    IEnumerator FadeAnimation()
    {
        Fadeout();
        yield return new WaitForSeconds(0.7f);
        Change();
        yield return new WaitForSeconds(0.5f);
        FadeIn();
        yield return new WaitForSeconds(0.5f);
        yield break;
    }

    void Fadeout()
    {
        disinfectantCard.DOMove(fadeOutPoint, 0.3f);
    }

    void FadeIn()
    {
        disinfectantText.text = next;
        next = "";
        disinfectantCard.DOMove(originPoint, 0.3f);
        currentCard.DOSizeDelta(originSize, 0.3f);
        currentDisinfectantImage.DOColor(originColor, 0.3f);
    }

    void Change()
    {
        disinfectantText.text = "";
        currentDisinfectantImage.color = nextDisinfectantImage.color;
        currentCard.sizeDelta = nextCard.sizeDelta;
        disinfectantCard.position = nextDisinfectantCard.position;
    }
}
