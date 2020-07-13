using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Joystick joystick;
    private Animator anim;
    public GameObject model;
    public float speed;

    public enum ePlayerState
    {
        Idle,
        Attack,
        Move,
        End
    }
    public ePlayerState eState { get; private set; }

    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        //anim = model.GetComponent<Animator>();
    }
    void Update()
    {
        anim = model.GetComponent<Animator>();
        if (joystick.Vertical != 0 || joystick.Horizontal != 0)
        {
            this.anim.Play("Run");
            this.transform.Translate(Vector3.forward * this.speed * Time.deltaTime);
            this.transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg, 0f);
        }
        else
            this.anim.Play("Idle");
    }
    private void SetPlayerState(ePlayerState state)
    {
        eState = state;
    }
}
