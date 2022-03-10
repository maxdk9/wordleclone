using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Random = System.Random;

public class GameData : MonoBehaviour
{
    
    private List<String> words=new List<string>();
    private int currentWordIndex;
    private String currentWord;

    private void Awake()
    {
        ReadFile();
        MainMenu.startNewGame.AddListener(SetCurrentWord);
        Statistic.startNewGame.AddListener(SetCurrentWord);
    }

    private void ReadFile()
    {
        TextAsset tAsset = Resources.Load<TextAsset>("words");
        if (tAsset == null)
        {
            throw new ApplicationException("Words file is not accessible");
        }

        words = tAsset.text.Split('\n').Select(text => text.Trim()).ToList();
        String s = "sdfsdf";
    }

    public List<String> GetWords()
    {
        return words;
    }


    public String GetCurrentWord()
    {
        return currentWord;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWordIndex = FindObjectOfType<GameStatSaver>().GetCurrentWordIndex();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentWord()
    {
        int wordIndex = UnityEngine.Random.Range(0, words.Count);
        currentWord = words[wordIndex];
        Debug.Log(currentWord);
    }
}
