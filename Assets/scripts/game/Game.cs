namespace keke
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(SceneController))]
    [RequireComponent(typeof(PlayerManager))]
    public class Game: Singleton<Game>
    {
        public string firstScene = "";
        public GameData data;

        [HideInInspector]
        public Vector2 playerMovement = new Vector2(0,0);
        
        [HideInInspector]
        public bool isPlayerMoving = false;
        private SceneController sceneController;
        private PlayerManager playerManager;

        private SceneController.OnSceneChanged sceneChangeCallback;


        public override void Init()
        {
            Debug.Log("Game/Init, firstScene = " + firstScene);
            base.Init();
            sceneController = gameObject.GetComponent<SceneController>();
            sceneChangeCallback = onSceneChanged;
            playerManager = PlayerManager.Instance;

            if(firstScene != "")
            {
                ChangeScene(firstScene);
            }
        }

        public void ChangeScene(string sceneName)
        {
            Debug.Log("Game/ChangeScene, sceneName = " + sceneName);
            sceneController.ChangeSceneAsync(sceneName, sceneChangeCallback);
        }

        private void onSceneChanged() 
        {
            Debug.Log("Game/onSceneChanged");
            if(this.sceneController.IsPlayerScene) 
            {
                playerManager.InitPlayerScene();
            }
            else if(playerManager.isPlayerActive)
            {
                playerManager.DestroyPlayerScene();
            }
        }

    }    
}