namespace keke
{
	using System;
	using System.Collections;
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public class SceneController
	{
		public delegate void OnSceneChanged ();

		protected Scene currentScene;
		protected SceneAgent currentSceneAgent;
		protected SceneType currentSceneType;

		public SceneController ()
		{
		}

		public string GetSceneName ()
		{
			return currentScene.name;
		}

		public bool GetIsPlayerScene ()
		{
			return (currentSceneAgent.type == SceneType.PLAYER) ? true : false;
		}

		public void InitScene ()
		{
			Debug.Log ("SceneController/InitScene");
			currentScene = SceneManager.GetActiveScene();
			currentSceneAgent = GameObject.Find ("scene").GetComponent<SceneAgent> ();
			currentSceneAgent.Init ();
		}

		public void SwitchScene (string scene, OnSceneChanged callback = null)
		{
			ChangeScene (scene, LoadSceneMode.Single, callback);
		}

		public void AddScene (string scene, OnSceneChanged callback = null)
		{
			ChangeScene (scene, LoadSceneMode.Additive, callback);
		}

		public void ChangeScene (string scene, LoadSceneMode mode, OnSceneChanged callback = null)
		{
			SceneManager.LoadScene (scene, mode);

			if (callback != null) {
				callback ();
			}
		}

		public void ChangeSceneAsync (string scene, OnSceneChanged callback = null)
		{
			
		}

		protected IEnumerator _changeSceneAsync (string scene, OnSceneChanged callback = null)
		{
			Scene oldScene = SceneManager.GetActiveScene ();
			Scene newScene = SceneManager.GetSceneByName (scene);

			Camera camera = Camera.main;
			camera.tag = "Untagged";

			AsyncOperation asyncOperation = SceneManager.LoadSceneAsync (scene, LoadSceneMode.Additive);

			while (!asyncOperation.isDone) {
				if (asyncOperation.progress >= 0.9f) {
					camera.gameObject.SetActive (false);

					GameObject.FindObjectOfType<AudioListener> ().enabled = false;

					foreach (GameObject go in oldScene.GetRootGameObjects()) {
						go.SetActive (false);
					}
				}

				yield return null;
			}

			SceneManager.SetActiveScene (newScene);

			if (callback != null) {
				callback ();
			}
		}
	}
}