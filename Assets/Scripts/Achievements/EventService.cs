using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;

namespace Events {
    public class EventService : GenericNonMonoSingleton<EventService>
    {
        public event Action onPlayerFiredBullet;
        public event Action onEnemyDeath;
        public event Action<string> onAchievementUnlocked;

        public void InvokePlayerFiredEvent() {
            onPlayerFiredBullet?.Invoke();
        }

        public void InvokeEnemyDeathEvent() {
            onEnemyDeath?.Invoke();
        }

        public void InvokeAchievementUnlockedEvent(string achievementText) {
            onAchievementUnlocked?.Invoke(achievementText);
        }
    }
    
}
