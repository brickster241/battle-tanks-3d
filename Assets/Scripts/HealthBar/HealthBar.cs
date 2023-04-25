using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HealthServices {
    public class HealthBar : MonoBehaviour
    {
        Transform MainCameraRig;
        [SerializeField] Image fill;
        // Start is called before the first frame update
        void Start()
        {
            MainCameraRig = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInParent<Transform>();
            fill.fillAmount = 1f;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.LookAt(MainCameraRig);
        }

        public void UpdateFill(float currentHealth, float totalHealth) {
            fill.fillAmount = currentHealth / totalHealth;
        }


    }

}
