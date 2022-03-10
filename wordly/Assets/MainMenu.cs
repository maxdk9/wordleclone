using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
    public static UnityEvent startNewGame=new UnityEvent();
    
    
    private GameData gameData;
    private void Awake()
    {
        gameData = FindObjectOfType<GameData>();
        startNewGame.AddListener(Hide);
        this.transform.DOLocalMoveY(0.0f,0.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        startNewGame.Invoke();
    }


    public void Hide()
    {
        transform.DOLocalMoveY(2000.0f, 1.25f).SetEase(Ease.OutBack);
    }

    public void Show()
    {
        transform.DOLocalMoveY(0.0f, 1.25f).SetEase(Ease.OutBack);
    }

}
