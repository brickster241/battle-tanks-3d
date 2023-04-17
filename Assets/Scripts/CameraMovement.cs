using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform TankPlayer;

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
