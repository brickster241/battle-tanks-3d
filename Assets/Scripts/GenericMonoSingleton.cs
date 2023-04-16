using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
{
    private static T instance;
    public static T Instance {get {return instance;}}

    private void Awake() {
        if (instance == null) {
            instance = (T)this;
            DontDestroyOnLoad(gameObject);
        } else {
            Debug.LogError("Instance is Not NULL. Cannot create Multiple Objects of Singleton.");
            Destroy(gameObject);
        }
    }
}
