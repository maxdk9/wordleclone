using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using DefaultNamespace;
using Model;
using UnityEngine;
using UnityEngine.UI;


public class GameField : MonoBehaviour
{
    private Line[] lines;
    private int currentLine;
    private bool gameEnd;
    private Statistic statistic;
    private ErrorPopup errorPopup;
    private GameData gameData;

    private void Awake()
    {
        lines = GetComponentsInChildren<Line>();
        gameData = FindObjectOfType<GameData>();
        statistic = FindObjectOfType<Statistic>();
        errorPopup = FindObjectOfType<ErrorPopup>();
        errorPopup.Hide();
        
        MainMenu.startNewGame.AddListener(Reset);
        Statistic.startNewGame.AddListener(Reset);
    }
    
    public bool isValidInput()
    {
        String validationResult = lines[currentLine].isValidInput(gameData.GetWords());
        bool valid = "".Equals(validationResult);
        if (!valid)
        {
            errorPopup.ShowPopup(validationResult);
        }

        return valid;
    }

    public void CheckAnswer()
    {
        if (gameEnd)
        {
            return;
        }
        Line line = lines[currentLine];
        string word = gameData.GetCurrentWord();
        bool isValid = line.CheckAnswer(word);
        if (isValid)
        {
            gameEnd = true;
            StartCoroutine(DelayedSuccess());
            
            
            IEnumerator DelayedSuccess()
            {
                yield return new WaitForSeconds(Line.TransactionDuration*.7f*2);
                line.PlaySuccess();
                yield return new WaitForSeconds(Line.TransactionDuration*3f);
                statistic.ShowOnWin(word,currentLine);
            }
        
        }
        else if(++currentLine==lines.Length)
        {
            gameEnd = true;
            StartCoroutine(DelayedFailure());

            IEnumerator DelayedFailure()
            {
                yield return new WaitForSeconds(Line.TransactionDuration*.7f*2);
                statistic.ShowOnFailure(word);
                
            }
        }
    }


    public void EnterLetter(LetterControl control)
    {
        if (gameEnd)
        {
            return;
        }
        lines[currentLine].EnterLetter(control);
    }

    public void RemoveLastLetter()
    {
        if (gameEnd)
        {
            return;
        }
        lines[currentLine].RemoveLast();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrUtils.WasPlayedToday())
        {
            GetComponentsInChildren<Image>(true).Last().gameObject.SetActive(true);
        }
    }


    public void Reset()
    {
        this.currentLine = 0;
        this.gameEnd = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    
}
