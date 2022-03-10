using System;
using UnityEngine;

namespace DefaultNamespace
{
    public static class ColorConstants
    {
        public  static Color rightColor=Color.green; 
        public static Color wrongPlaceColor = Color.yellow;
        public static Color wrongColor = Color.gray;
        public static Color defaultButtonColor=Color.blue;
        public static Color keyboardColor=HexToColor("#3484D6");
        public static Color letterColor=HexToColor("#81B5EA");

        public static Color HexToColor(String hex)
        {
            Color color;
            if (ColorUtility.TryParseHtmlString(hex, out color))
                return color;

            return Color.white;

        }
    }
}