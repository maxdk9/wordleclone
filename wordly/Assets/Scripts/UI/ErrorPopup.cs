using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


    public class ErrorPopup : UnityEngine.MonoBehaviour
    {
        private Image image;
        private TextMeshProUGUI text;

        private void Awake()
        {
            image = GetComponentInChildren<Image>();
            text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void ShowPopup(string t)
        {
            this.gameObject.SetActive(true);
            text.text = t;
            image.DOColor(Color.black, .5f);
            text.DOColor(Color.white, .51f).onComplete= () =>
                   {
                       DOTween.Sequence().SetDelay(.75f).Append(image.DOColor(Color.clear, .5f))
                           .Join(text.DOColor(Color.clear, .5f));   
                }            
                ;
        }


        public void Hide()
        {
           this.gameObject.SetActive(false);
        }
    }
    
    
    
    
