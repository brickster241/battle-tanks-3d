using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraService {
    public class CameraMovement : MonoBehaviour
    {
        Transform TankPlayer;

        private void Start() {
            TankPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        // Update is called once per frame
        void LateUpdate()
        {   
            Vector3 cameraAngles = transform.eulerAngles;
            cameraAngles.y = TankPlayer.eulerAngles.y;
            transform.eulerAngles = cameraAngles;
            StartCoroutine(UpdateCameraMovement());
        }

        IEnumerator UpdateCameraMovement() {
            while (transform.position != TankPlayer.position) {
                transform.position = Vector3.MoveTowards(transform.position, TankPlayer.position, Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }

}
