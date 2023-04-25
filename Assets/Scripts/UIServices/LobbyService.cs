using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Generics;
using GameAudio;

namespace GameUI {
    public class LobbyService : GenericMonoSingleton<LobbyService>
    {
        [SerializeField] GameObject MainCamera;

        private void Start() {
            StartCoroutine(AnimateCamera());
        }

        IEnumerator AnimateCamera() {
            while (true) {
                while (MainCamera.transform.eulerAngles.y <= 90) {
                    Vector3 eulerAngles = MainCamera.transform.eulerAngles;
                    eulerAngles.y += Time.deltaTime * 10;
                    MainCamera.transform.eulerAngles = eulerAngles;
                    yield return new WaitForEndOfFrame();
                }
                while (MainCamera.transform.eulerAngles.y >= 10) {
                    Vector3 eulerAngles = MainCamera.transform.eulerAngles;
                    eulerAngles.y -= Time.deltaTime * 10;
                    MainCamera.transform.eulerAngles = eulerAngles;
                    yield return new WaitForEndOfFrame();
                }
                yield return null;
            }
        }

        public void Play() {
            AudioService.Instance.PlayAudio(GameAudio.AudioType.BUTTON_CLICK);
            AudioService.Instance.PlayAudio(GameAudio.AudioType.LEVEL_BG);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Quit() {
            AudioService.Instance.PlayAudio(GameAudio.AudioType.BUTTON_CLICK);
            Application.Quit();
        }
    }

}
