using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Generics;
using Events;
using GameAudio;

namespace GameUI {
    public class UIService : GenericMonoSingleton<UIService>
    {
        [SerializeField] private GameObject achievementUI;
        [SerializeField] private GameObject gameOverUI;
        Queue<string> AchievementList = new Queue<string>();
        [SerializeField] private TextMeshProUGUI achievementText;

        private void OnEnable() {
            EventService.Instance.onAchievementUnlocked +=  AchievementUnlocked;
            EventService.Instance.onPlayerDeath += DisplayGameOverUI;
        }

        public void AchievementUnlocked(string Achievement_Text) {
            AchievementList.Enqueue(Achievement_Text);
            if (AchievementList.Count == 1)
                StartCoroutine(DisplayAchievements());
        }

        IEnumerator DisplayAchievements() {
            while (AchievementList.Count != 0) {
                achievementText.text = AchievementList.Peek();
                achievementUI.SetActive(true);
                AudioService.Instance.PlayAudio(GameAudio.AudioType.ACHIEVEMENT_UNLOCKED);
                yield return new WaitForSeconds(2.5f);
                achievementUI.SetActive(false);
                yield return new WaitForSeconds(1f);
                AchievementList.Dequeue();
            }
            
        }

        public void RestartLevel() {
            AudioService.Instance.PlayAudio(GameAudio.AudioType.BUTTON_CLICK);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void MainMenu() {
            AudioService.Instance.PlayAudio(GameAudio.AudioType.BUTTON_CLICK);
            SceneManager.LoadScene(0);
        }

        public void DisplayGameOverUI() {
            AudioService.Instance.StopAudio(GameAudio.AudioType.LEVEL_BG);
            gameOverUI.SetActive(true);
        }

        private void OnDisable() {
            EventService.Instance.onAchievementUnlocked -= AchievementUnlocked;
            EventService.Instance.onPlayerDeath -= DisplayGameOverUI; 
        }
    }
}
