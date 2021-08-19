using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField]private Rigidbody2D ball_rigi;
    [SerializeField]private SpringJoint2D ball_sprjt;
    [SerializeField]private float detach_time;
    private Camera camera_main;
    private bool dragging;
    // Start is called before the first frame update
    void Start()
    {
        camera_main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(ball_rigi == null)
        {
            return;
        }
        if(!Touchscreen.current.primaryTouch.press.isPressed)
        {
            if(dragging)
            {
                Launch_Ball();
            }
            return;
        }
        else
        {
            dragging = true;
            Vector2 touch_pos = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 world_pos = camera_main.ScreenToWorldPoint(touch_pos);
            ball_rigi.position = world_pos;
            ball_rigi.isKinematic = true;
        }
    }

    private void Launch_Ball()
    {
        ball_rigi.isKinematic = false;
        ball_rigi = null;
        // Invoke("Detach_Ball",detach_time); 與下一行同義
        Invoke(nameof(Detach_Ball), detach_time);
    }
    public void Detach_Ball()
    {
        ball_sprjt.enabled = false;
        ball_sprjt = null;
    }
}
