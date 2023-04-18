using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generics {
    public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
    {
        private static T instance;
        public static T Instance {get {return instance;}}

        protected virtual void Awake()
        {
            if(instance != null)
            {
                Destroy(this);
            }
            else
            {
                instance = (T)this;
            }
        }
    }

}
