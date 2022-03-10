using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Line : MonoBehaviour
{
    
        
    private Image[] images;
    private TextMeshProUGUI[] texts;
    public static  float TransactionDuration=0.15f;
    private int currentField;
    private LetterControl[] letterControls;


    private void Awake()
    {
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<TextMeshProUGUI>();
        letterControls=new LetterControl[images.Length];
        MainMenu.startNewGame.AddListener(Reset);
        Statistic.startNewGame.AddListener(Reset);
    }

    public String isValidInput(List<String> allWords)
    {
        var errorMessage = "";
        for (int i = 0; i < images.Length; i++)
        {
            if ("".Equals(texts[i].text))
            {
                images[i].transform.DOKill(true);
                images[i].transform.DOShakePosition(.75f, new Vector3(10, 0));
                errorMessage = "No enough letter";
            }
        }

        String text = texts.Aggregate("", (current, textMesh) => current + textMesh.text).ToLowerInvariant();
        if ("".Equals(errorMessage) && !allWords.Contains(text))
        {
            errorMessage = "Not in word list";
            foreach (Image image in images)
            {
                image.transform.DOKill(true);
                image.transform.DOShakePosition(.75f, new Vector3(10, 0));
            }
        }

        return errorMessage;
    }

    public bool CheckAnswer(String correctWord)
    {
        var text = texts.Aggregate("", (current, textMesh) => current + textMesh.text).ToLowerInvariant();
        Color [] letterColors=new Color[texts.Length];
        String remCorrectWord = correctWord;
        for (int i = 0; i < text.Length; i++)
        {
            char letter = text[i];
            char correct = correctWord[i];
            if (correct == letter)
            {
                letterColors[i] = ColorConstants.rightColor;
                remCorrectWord=remCorrectWord.Remove(i, 1).Insert(i, "_");
                continue;
            }

            
            
            if (remCorrectWord.Contains("" + letter))
            {
                letterColors[i] = ColorConstants.wrongPlaceColor;
            }
            else
            {
                letterColors[i] = ColorConstants.wrongColor;
            }
        }

        float delay = 0;
        for (int i = 0; i < images.Length; i++)
        {

            Image image = images[i];
            DOTween.Sequence().SetDelay(delay).Append(image.transform.DOScale(0, TransactionDuration))
                .Append(image.DOColor(letterColors[i], 0))
                .Append(texts[i].DOColor(Color.white, 0))
                .Append(image.transform.DOScale(1, TransactionDuration));
            letterControls[i].ChangeColorControl(letterColors[i]);
            delay += TransactionDuration * 2;
            
        }
        
        
        return correctWord.Equals(text);
    }

    public void PlaySuccess()
    {
        foreach (var image in images)
        {
            image.transform.DOPunchPosition(new Vector3(0, 40), TransactionDuration * 2).SetEase(Ease.InOutCirc);
        }
        
    }
    
    

    public void EnterLetter(LetterControl control)
    {
        
        images[currentField].transform.DOPunchScale(new Vector3(0.15f, 0.15f), 0.2f, 1, 0);
        
        letterControls[currentField] = control;
        
        texts[currentField].text = control.GetLetter();
        currentField++;
    }

    public void RemoveLast()
    {
        if (currentField <= 0)
        {
            return;
        }
        currentField--;
        texts[currentField].text = "";
        
        
        
    }

    public void Reset()
    {
        foreach (var text in texts)
        {
            text.text = "";
        }

        foreach (var VARIABLE in this.images)
        {
            VARIABLE.color = ColorConstants.letterColor;
        }
        currentField = 0;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
