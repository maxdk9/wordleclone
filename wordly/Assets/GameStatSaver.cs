using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Model;
using UnityEngine;

public class GameStatSaver : MonoBehaviour
{

    private String statsPath;

    public Stats SaveSuccessStats(int linenumber)
    {
        Stats data = LoadStatsData();
        data.successes++;
        data.lineSuccessStats[linenumber]++;
        data.currentWordIndex++;
        data.currentStreak++;
        if (data.currentStreak > data.maxStreak)
        {
            data.maxStreak = data.currentStreak;
        }
        SaveFile(statsPath,data);
        return data;
    }

    public Stats SaveFailureData()
    {
        Stats data = LoadStatsData();
        data.failures++;
        data.currentStreak = 0;
        data.currentWordIndex++;
        SaveFile(statsPath,data);
        return data;
    }
    
    private void Awake()
    {
        statsPath = Path.Combine(Application.persistentDataPath, "st.dat");
        if (!File.Exists(statsPath))
        {
            CreateNewStatsFile();
        }
    }

    public int GetCurrentWordIndex()
    {
        return LoadStatsData().currentWordIndex;
    }

    private Stats LoadStatsData()
    {
        return JsonUtility.FromJson<Stats>(File.ReadAllText(statsPath));
    }

    private static void SaveFile(string filePath, object data)
    {
        File.WriteAllText(filePath,JsonUtility.ToJson(data));
    }


    private void CreateNewStatsFile()
    {
        SaveFile(statsPath,new Stats
        {
            lineSuccessStats = new int[6]
        });
    }
}
