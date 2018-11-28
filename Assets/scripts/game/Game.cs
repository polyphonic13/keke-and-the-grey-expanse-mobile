namespace keke
{
    using System;
    using UnityEngine;

    public class Game: Singleton<Game>
    {
        public GameData data;
        public Vector2 playerVelocity = new Vector2(0, 0);

        private SceneController.OnSceneChanged _sceneChangeCallback;

        void Awake()
        {
            this._sceneChangeCallback = _onSceneChanged;
        }

        private void _onSceneChanged() {
            Debug.Log("Game/_onSceneChanged");
        }
    }    
}