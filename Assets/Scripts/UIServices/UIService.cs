using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Generics;
using Events;

namespace GameUI {
    public class UIService : GenericMonoSingleton<UIService>
    {
        [SerializeField] private GameObject achievementUI;
        Queue<string> AchievementList = new Queue<string>();
        [SerializeField] private TextMeshProUGUI achievementText;

        private void OnEnable() {
            EventService.Instance.onAchievementUnlocked +=  AchievementUnlocked;
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
                yield return new WaitForSeconds(2.5f);
                achievementUI.SetActive(false);
                yield return new WaitForSeconds(1f);
                AchievementList.Dequeue();
            }
            
        }

        private void OnDisable() {
            EventService.Instance.onAchievementUnlocked -= AchievementUnlocked;    
        }
    }
}
