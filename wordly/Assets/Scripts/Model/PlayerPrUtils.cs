using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Model
{
    public static class PlayerPrUtils
    {
        private const string LastPlayed = "LastPlayed";
        private const string DateFormat = "dd/MM/yyyy";

        public static bool WasPlayedToday()
        {
            return PlayerPrefs.HasKey(LastPlayed) &&
                   FromBase64(PlayerPrefs.GetString(LastPlayed)).Equals(DateTime.Now.ToString(DateFormat));
        }


        public static void SetPlayedToday()
        {
            PlayerPrefs.SetString(LastPlayed,ToBase64(DateTime.Now.ToString(DateFormat)));
        }

        private static String FromBase64(string value)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }

        private static String ToBase64(string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }

        
        
    }
}