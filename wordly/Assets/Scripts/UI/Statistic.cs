using System;
using System.Collections.Generic;
using System.ComponentModel;
using DG.Tweening;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Statistic : MonoBehaviour
    {
        public static UnityEvent startNewGame=new UnityEvent();
        
        [SerializeField]
        private Image answerImage;
        [SerializeField]
        private TextMeshProUGUI answer;
        [SerializeField]
        private TextMeshProUGUI played;
        [SerializeField]
        private TextMeshProUGUI win;
        [SerializeField]
        private TextMeshProUGUI currentStreak;
        [SerializeField]
        private TextMeshProUGUI maxStreak;
        [SerializeField]
        private DistributionData[] distributionLines;
        private GameStatSaver gameStatSaver;
        private Stats stats;

        private void Awake()
        {
            gameStatSaver = FindObjectOfType<GameStatSaver>();
            startNewGame.AddListener(Hide);
        }

        public void ShowOnWin(string word,int currentLine)
        {
            answer.text = word;
            answerImage.color = ColorConstants.rightColor;
            stats = gameStatSaver.SaveSuccessStats(currentLine);
            PlayerPrUtils.SetPlayedToday();
            ExecuteOnShow(stats, currentLine);
        }

        public void ShowOnFailure(string word)
        {
            answer.text = word;
            answerImage.color = ColorConstants.wrongColor;
            stats = gameStatSaver.SaveFailureData();
            PlayerPrUtils.SetPlayedToday();
            ExecuteOnShow(stats);
        }

        private void ExecuteOnShow(Stats savedStats, int currentLine=-1)
        {
            int totalPlayed = savedStats.failures + savedStats.successes;
            played.text = totalPlayed.ToString();
            float winrate = totalPlayed == 0 ? 0 : savedStats.successes / (float) totalPlayed * 100;
            win.text = winrate.ToString();
            currentStreak.text = savedStats.currentStreak.ToString();
            maxStreak.text = savedStats.maxStreak.ToString();
            
            SortedDictionary<int,List<int>> sortedDictionary=new SortedDictionary<int, List<int>>();
            for (int i = 0; i < savedStats.lineSuccessStats.Length; i++)
            {
                int countForLine = savedStats.lineSuccessStats[i];
                distributionLines[i].Setup(countForLine, currentLine == i);

                if (sortedDictionary.ContainsKey(countForLine))
                {
                    sortedDictionary[countForLine].Add(i);
                }
                else
                {
                    sortedDictionary.Add(countForLine,new List<int>{i});
                }
                
            }

            int[] sizes = new[] {1000, 800, 600, 300, 140};
            int sizeIterator = 0;

            foreach (var dictEl in sortedDictionary)
            {
                if (dictEl.Key == 0)
                {
                    continue;
                }

                foreach (int i in dictEl.Value)
                {
                    distributionLines[i].SetSize(sizes[sizeIterator]);
                }

                sizeIterator++;
            }

            Show();
        }


        public void Show()
        {
            transform.DOLocalMoveY(0.0f, 1.25f).SetEase(Ease.OutBack);
        }

        public void Hide()
        {
            transform.DOLocalMoveY(2000.0f, 1.25f).SetEase(Ease.OutBack);
        }

        public void StartNewGame()
        {
            startNewGame.Invoke();    
        }
        
        
    }
}