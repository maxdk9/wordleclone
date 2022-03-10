using System;
using System.Net.Mime;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LetterControl : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI text;
    private Control control;

    private void Awake()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        control = GetComponentInParent<Control>();
        MainMenu.startNewGame.AddListener(Reset);
        Statistic.startNewGame.AddListener(Reset);
    }


    public String GetLetter()
    {
        return text.text.ToLowerInvariant();
    }

    public void EnterLetter()
    {
        control.EnterLetter(this);
    }

    public void ChangeColorControl(Color letterColor)
    {
        image.DOColor(letterColor,.2f);
    }

    public void Reset()
    {
        image.color = ColorConstants.keyboardColor;
    }
}
