using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueUI : MonoBehaviour
{
    public TextMeshProUGUI ui_name { get; set; }
    public TextMeshProUGUI ui_word { get; set; }
    public Image ui_model { get; set; }
    public Button ui_nextButton{ get; set; }

    public void DialogueEffect(string word, float delay) => StartCoroutine(EffectCorutine(word, delay));

    IEnumerator EffectCorutine(string word, float delay)
    {
        WaitForSeconds delays = new WaitForSeconds(delay);

        for(int i = 0; i < word.Length; i++)
        {
            string text = word.Substring(0, i);
            ui_word.text = text;
            yield return delay;
        }

        ui_nextButton.gameObject.SetActive(true);
    }
}
