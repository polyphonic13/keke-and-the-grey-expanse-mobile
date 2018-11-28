namespace keke
{
    using System;

    [System.Serializable]
    public class GameData {
        public int level = 0;
        public int score = 0;
        public bool isMusicOn = true;

        public GameData Clone() {
            GameData clone = new GameData();
            clone.level = this.level;
            clone.score = this.score;
            clone.isMusicOn = this.isMusicOn;

            return clone;
        }

    }
}