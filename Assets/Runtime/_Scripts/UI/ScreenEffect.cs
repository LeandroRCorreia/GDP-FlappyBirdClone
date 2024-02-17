using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class ScreenEffect : MonoBehaviour
{
    [Header("Fade Params")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private Image image;

    

    public IEnumerator Flash()
    {
        yield return StartCoroutine(FadeIn(0.05f, Color.black));
        yield return FadeOut(0.05f, Color.black);
        image.enabled = false;

    }

    public IEnumerator FadeIn(float fadeTime, Color colorIn)
    {
        image.enabled = true;
        fadeCanvasGroup.alpha = 0;
        image.color = colorIn;
        Tween imageTween = image.DOColor(colorIn, fadeTime);
        Tween fadeTween = fadeCanvasGroup.DOFade(1, fadeTime);

        yield return fadeTween.WaitForCompletion();
        image.enabled = false;

    }

    public IEnumerator FadeOut(float fadeTime, Color colorOut)
    {
        image.enabled = true;

        fadeCanvasGroup.alpha = 1;
        image.color = colorOut;
        Tween imageTween = image.DOColor(colorOut, fadeTime);
        Tween fadeTween = fadeCanvasGroup.DOFade(0, fadeTime);


        yield return fadeTween.WaitForCompletion();
        image.enabled = false;

    }

}
