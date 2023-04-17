using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] float TANK_FORCE;
    [SerializeField] float TANK_ROTATION_SPEED;
    bool isReverse = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) {
            isReverse = false;
            transform.Translate(transform.forward.normalized * TANK_FORCE * Time.deltaTime, Space.World);
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            isReverse = true;
            transform.Translate(-transform.forward.normalized * TANK_FORCE * Time.deltaTime, Space.World);
        } else {
            isReverse = false;
            transform.Translate(Vector3.zero, Space.World);
        }

        int factor = (isReverse) ? -1 : 1;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate(new Vector3(0, -factor * TANK_ROTATION_SPEED * Time.deltaTime, 0), Space.World);
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate(new Vector3(0, factor * TANK_ROTATION_SPEED * Time.deltaTime, 0), Space.World);
        } else {
            transform.Rotate(Vector3.zero, Space.World);
        }
    }
}
