using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Generics;

namespace Achievements {
    public class AchievementService : GenericMonoSingleton<AchievementService>
    {
        private int bulletFiredByPlayerCount;
        private int EnemyKilledCount;
        private EventService eventService;

        private void OnEnable() {
            EventService.Instance.onEnemyDeath += CheckEnemyDeathAchievement;
            EventService.Instance.onPlayerFiredBullet += CheckBulletFiredAchievement;
        }

        protected override void Awake() {
            base.Awake();
            bulletFiredByPlayerCount = 0;
            EnemyKilledCount = 0;
        }

        public void CheckBulletFiredAchievement() {
            bulletFiredByPlayerCount += 1;
            if (bulletFiredByPlayerCount % 10 == 0) {
                string achievementText = bulletFiredByPlayerCount + " BULLETS FIRED :)";
                EventService.Instance.InvokeAchievementUnlockedEvent(achievementText);
            }
        }

        public void CheckEnemyDeathAchievement() {
            EnemyKilledCount += 1;
            if (EnemyKilledCount % 5 == 0) {
                string achievementText = EnemyKilledCount + " ENEMIES KILLED :)";
                EventService.Instance.InvokeAchievementUnlockedEvent(achievementText);
            }
        }

        private void OnDisable() {
            EventService.Instance.onEnemyDeath -= CheckEnemyDeathAchievement;
            EventService.Instance.onPlayerFiredBullet -= CheckBulletFiredAchievement;
        }
    }
}
