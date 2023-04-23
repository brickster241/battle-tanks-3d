using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Generics;

namespace Achievements {
    public class AchievementService : GenericMonoSingleton<AchievementService>
    {
        public int bulletFiredByPlayerCount;
        public int EnemyKilledCount;
        private EventService eventService;

        private void OnEnable() {
            EventService.Instance.onEnemyDeath += CheckEnemyDeathAchievement;
            EventService.Instance.onPlayerFiredBullet += CheckBulletFiredAchievement;
        }

        protected override void Awake() {
            base.Awake();
            bulletFiredByPlayerCount = 0;
            EnemyKilledCount = 0;
            eventService = new EventService();
        }

        public void CheckBulletFiredAchievement() {
            bulletFiredByPlayerCount += 1;
            if (bulletFiredByPlayerCount == 5) {
                Debug.Log("ACHIEVEMENT UNLOCKED : 5 BULLETS FIRED");
            } else if (bulletFiredByPlayerCount == 10) {
                Debug.Log("ACHIEVEMENT UNLOCKED : 10 BULLETS FIRED");
            } else if (bulletFiredByPlayerCount == 15) {
                Debug.Log("ACHIEVEMENT UNLOCKED : 15 BULLETS FIRED");
            }
        }

        public void CheckEnemyDeathAchievement() {
            EnemyKilledCount += 1;
            if (EnemyKilledCount == 5) {
                Debug.Log("ACHIEVEMENT UNLOCKED : 5 ENEMY DEATHS");
            } else if (EnemyKilledCount == 10) {
                Debug.Log("ACHIEVEMENT UNLOCKED : 10 ENEMY DEATHS");
            } else if (EnemyKilledCount == 15) {
                Debug.Log("ACHIEVEMENT UNLOCKED : 15 ENEMY DEATHS");
            }
        }

        private void OnDisable() {
            EventService.Instance.onEnemyDeath -= CheckEnemyDeathAchievement;
            EventService.Instance.onPlayerFiredBullet -= CheckBulletFiredAchievement;
        }
    }
}
