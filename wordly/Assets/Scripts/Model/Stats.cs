using System;

namespace Model
{
    [Serializable]
    public class Stats
    {
        public int[] lineSuccessStats;
        public int currentWordIndex;
        public int successes;
        public int failures;
        public int currentStreak;
        public int maxStreak;
        
    }
}