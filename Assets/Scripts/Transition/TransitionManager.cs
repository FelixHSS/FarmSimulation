using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Farm.Transition
{
    public class TransitionManager : MonoBehaviour
    {
        [field: SerializeField] public string StartSceneName { get; set; } = string.Empty;

        private void OnEnable()
        {
            EventHandler.TransitionEvent += OnTransitionEvent;
        }

        private void OnDisable()
        {
            EventHandler.TransitionEvent -= OnTransitionEvent;
        }



        private void Start()
        {
            StartCoroutine(LoadSceneSetActive(StartSceneName));
        }


        private void OnTransitionEvent(string sceneToGo, Vector3 positionToGo)
        {
            StartCoroutine(Transition(sceneToGo, positionToGo));
        }

        /// <summary>
        /// Switch scenes
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        private IEnumerator Transition(string sceneName, Vector3 targetPosition)
        {
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            yield return LoadSceneSetActive(sceneName);
        }

        /// <summary>
        /// Load the scene and set it active
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        private IEnumerator LoadSceneSetActive(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

            SceneManager.SetActiveScene(newScene);
        }
    }
}