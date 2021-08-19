using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField]private Rigidbody2D ball_rigi;
    private Camera camera_main;
    // Start is called before the first frame update
    void Start()
    {
        camera_main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Touchscreen.current.primaryTouch.press.isPressed)
        {
            ball_rigi.isKinematic = false;
            return;
        }
        else
        {
            Vector2 touch_pos = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 world_pos = camera_main.ScreenToWorldPoint(touch_pos);
            ball_rigi.position = world_pos;
            ball_rigi.isKinematic = true;
        }
    }
}
