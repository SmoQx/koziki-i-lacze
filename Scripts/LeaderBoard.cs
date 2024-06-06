namespace LeaderBoard
{
    using System;
    using UnityEngine;

    [System.Serializable]
    public class LeaderBoardEntry
    {
        public int Id { get; set; }
        public string Nick { get; set; }
        public int PoziomDoswiadczenia { get; set; }
        public int Zwyciestwa { get; set; }
        public int CzasGry { get; set; }

        [NonSerialized]
        public TimeSpan ParsedCzasGry;

        public void ParseCzasGry()
        {
            ParsedCzasGry = TimeSpan.Parse(CzasGry.ToString());
        }
    }
}