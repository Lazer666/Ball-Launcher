using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField]private GameObject ball_pre;
    [SerializeField]private Rigidbody2D pivot_rigi;
    private Rigidbody2D ball_rigi;
    private SpringJoint2D ball_sprjt;
    [SerializeField]private float detach_time,respawn_time;
    private Camera camera_main;
    private bool dragging;
    // Start is called before the first frame update
    void Start()
    {
        camera_main = Camera.main;
        Spawn_Ball();
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
            dragging = false;
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
        Invoke(nameof(Spawn_Ball),respawn_time);
    }
    private void Spawn_Ball()
    {
        GameObject ball_ins = Instantiate(ball_pre, pivot_rigi.position, Quaternion.identity);
        ball_rigi = ball_ins.GetComponent<Rigidbody2D>();
        ball_sprjt = ball_ins.GetComponent<SpringJoint2D>();
        ball_sprjt.connectedBody = pivot_rigi;
    }
}