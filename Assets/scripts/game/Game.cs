namespace keke
{
    using System;
    using UnityEngine;

    public class Game: Singleton<Game>
    {
        public GameData data;
        public Vector2 playerVelocity = new Vector2(0, 0);

        private SceneController sceneController;

        private SceneController.OnSceneChanged sceneChangeCallback;

        public override void Init()
        {
            Debug.Log("Game/Init");
            base.Init();
            this.sceneController = new SceneController();
            this.sceneChangeCallback = onSceneChanged;
        }

        public void ChangeScene(string scene)
        {
            sceneController.AddScene(scene, sceneChangeCallback);
        }

        private void onSceneChanged() 
        {
            Debug.Log("Game/onSceneChanged");
            if(this.sceneController.IsPlayerScene) 
            {

            }
            else
            {
                
            }
        }

    }    
}