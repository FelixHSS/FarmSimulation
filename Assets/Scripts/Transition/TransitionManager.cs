using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Farm.Transition
{
    public class TransitionManager : MonoBehaviour
    {
        [field: SceneName]
        [field: SerializeField] public string StartSceneName { get; set; } = string.Empty;
        private CanvasGroup FadeCanvasGroup { get; set; }
        private bool IsFade { get; set; }

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
            FadeCanvasGroup = FindObjectOfType<CanvasGroup>(); //only have one CanvasGroup in scene
        }


        private void OnTransitionEvent(string sceneToGo, Vector3 positionToGo)
        {
            if(!IsFade)
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
            EventHandler.CallBeforeSceneUnloadEvent();
            yield return Fade(1);
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            yield return LoadSceneSetActive(sceneName);
            EventHandler.CallMoveToPositionEvent(targetPosition); // move player
            yield return Fade(0);
            EventHandler.CallAfterSceneLoadedEvent();
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

        /// <summary>
        /// transition animation between scenes
        /// </summary>
        /// <param name="targetAlpha">1 means canvas is opaque, 0 is transparent</param>
        /// <returns></returns>
        private IEnumerator Fade(float targetAlpha)
        {
            IsFade = true;
            FadeCanvasGroup.blocksRaycasts = true;

            float speed = Mathf.Abs(FadeCanvasGroup.alpha - targetAlpha) / Settings.CanvasFadeDuration;

            while(!Mathf.Approximately(FadeCanvasGroup.alpha, targetAlpha))
            {
                FadeCanvasGroup.alpha = Mathf.MoveTowards(FadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
                yield return null;
            }

            FadeCanvasGroup.blocksRaycasts = false;
            IsFade = false;
        }
    }
}