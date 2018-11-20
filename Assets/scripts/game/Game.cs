namespace keke
{
    using System;
    using UnityEngine;

    public class Game: Singleton<Game>
    {
        public Vector2 playerVelocity = new Vector2(0, 0);
    }    
}