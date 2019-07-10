using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    public TMPro.TMP_Text interactText;
    public CanvasGroup subtitlesText;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    public void SetSubtitlesText(string text)
    {
        TMPro.TMP_Text textComponent = subtitlesText.GetComponentInChildren(typeof(TMPro.TMP_Text)).GetComponent<TMPro.TMP_Text>();
        textComponent.text = "";
        if (text != null)
        {
            
            textComponent.text = text;
        }
    }

    public void SetSubtitlesVisibility(bool isVisible)
    {
        if (isVisible)
        {
            subtitlesText.GetComponent<Animator>().Play("FadeInUI");
        }
        else
        {
            subtitlesText.GetComponent<Animator>().Play("FadeOutUI");
        }
    }

    public void PlaySubtitles(float delayTime, float textLengthTime)
    {
        StartCoroutine(SetSubtitlesFade(delayTime, textLengthTime));
    }

    private IEnumerator SetSubtitlesFade(float delayTime, float textLengthTime)
    {

        yield return new WaitForSeconds(delayTime);
        SetSubtitlesVisibility(true);
        yield return new WaitForSeconds(textLengthTime);
        SetSubtitlesVisibility(false);

    }

    public void SetInteractText(string text)
    {
        interactText.text = "E: " + text;
        if (text == "")
            interactText.text = "";
    }
}
