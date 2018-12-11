namespace keke
{
	using System;
	using System.Collections;
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public class SceneController: CoreObject
	{
		public delegate void OnSceneChanged ();
		protected Scene activeScene;
		protected SceneAgent activeSceneAgent;
		protected SceneType activeSceneType;

		private IEnumerator coroutine;

		public SceneController ()
		{
		}

		public string GetSceneName ()
		{
			return activeScene.name;
		}

		public bool IsPlayerScene
		{
			get {
				return (activeSceneAgent.type == SceneType.PLAYER) ? true : false;
			}
		}

		public void InitScene ()
		{
			Debug.Log ("SceneController/InitScene");
			activeScene = SceneManager.GetActiveScene();
			GameObject sceneGameObject = GameObject.Find("scene");
			Debug.Log("scene go = " + sceneGameObject);
			activeSceneAgent = sceneGameObject.GetComponent<SceneAgent> ();
			activeSceneAgent.Init ();
		}

		public void SwitchScene (string sceneName, OnSceneChanged callback = null)
		{
			ChangeScene (sceneName, LoadSceneMode.Single, callback);
		}

		public void AddScene (string sceneName, OnSceneChanged callback = null)
		{
			ChangeScene (sceneName, LoadSceneMode.Additive, callback);
		}

		public void ChangeScene (string sceneName, LoadSceneMode mode, OnSceneChanged callback = null)
		{
			Debug.Log("SceneController/ChangeScene, sceneName = " + sceneName + ", mode = " + mode);
			SceneManager.LoadScene (sceneName, mode);
			InitScene();
			if (callback != null) {
				callback ();
			}
		}

		public void ChangeSceneAsync (string sceneName, OnSceneChanged callback = null)
		{
			Debug.Log("SceneController/ChangeSceneAsync, sceneName = " + sceneName);
			coroutine = changeSceneAsync(sceneName, callback);
			StartCoroutine(coroutine);
		}

		protected IEnumerator changeSceneAsync (string sceneName, OnSceneChanged callback = null)
		{
			Debug.Log(" changeSceneAsync, sceneName = " + sceneName);
			Scene oldScene = SceneManager.GetActiveScene ();
			Scene newScene = SceneManager.GetSceneByName (sceneName);

			// Camera camera = Camera.main;
			// camera.tag = "Untagged";

			AsyncOperation asyncOperation = SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive);

			while (!asyncOperation.isDone) {
				if (asyncOperation.progress >= 0.9f) {
					// camera.gameObject.SetActive (false);

					GameObject.FindObjectOfType<AudioListener> ().enabled = false;

					foreach (GameObject go in oldScene.GetRootGameObjects()) {
						go.SetActive (false);
					}
				}

				yield return null;
			}
			Debug.Log(" newScene = " + newScene);
			// SceneManager.SetActiveScene (newScene);
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

			InitScene();

			if (callback != null) {
				callback ();
			}
		}
	}
}