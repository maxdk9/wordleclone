using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{

    private const int MaxValues = 5;

    private GameField gameField;
    private int valuesEntered;

    private void Awake()
    {
        gameField = FindObjectOfType<GameField>();
        
    }

    public void CheckAnswer()
    {
        if (!gameField.isValidInput())
        {
            return;
        }
        gameField.CheckAnswer();
        valuesEntered = 0;

    }


    public void RemoveLast()
    {
        if (valuesEntered == 0)
        {
            return;
        }
        gameField.RemoveLastLetter();
        valuesEntered--;
    }

    public void EnterLetter(LetterControl letterControl)
    {
        if (valuesEntered >= MaxValues)
        {
            return;
        }
        gameField.EnterLetter(letterControl);
        valuesEntered++;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
