using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DistributionData : MonoBehaviour
    {
        private TextMeshProUGUI count;
        private Image bar;


        private void Awake()
        {
            count = GetComponentsInChildren<TextMeshProUGUI>()[1];
            bar = GetComponentInChildren<Image>();
        }

        public void Setup(int countForLine, bool toColor)
        {
            count.text = countForLine.ToString();
            bar.color = toColor ? ColorConstants.rightColor : ColorConstants.wrongColor;

        }

        public void SetSize(int width)
        {
            bar.rectTransform.sizeDelta=new Vector2(width,100);
        }
    }
