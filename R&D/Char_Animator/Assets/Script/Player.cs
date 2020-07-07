using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Joystick joystick;
    [SerializeField]
    private Animator anim;
    public GameObject model;
    public float speed;


    // Test Code
    [SerializeField]
    private bool Joystick_test = false;
    [SerializeField]
    private Transform target;

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
        //this.anim.SetBool("Shoot", false);
    }

    private void Update()
    {
        anim = model.GetComponent<Animator>();

        if (this.anim.GetBool("Shoot") == true)
        {
            this.transform.LookAt(target);
            if (joystick.Vertical != 0 || joystick.Horizontal != 0)
            {
                this.anim.Play("Run");
                this.transform.Translate(new Vector3(joystick.Direction.x, 0, joystick.Direction.y) * this.speed * Time.deltaTime, Space.World);
            }
            else
                this.anim.Play("Idle");
        }
        else if (this.anim.GetBool("Shoot") == false)
        {
            if (joystick.Vertical != 0 || joystick.Horizontal != 0)
            {
                this.anim.Play("Run");
                this.transform.Translate(Vector3.forward * this.speed * Time.deltaTime);
                this.transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg, 0f);
            }
            else
                this.anim.Play("Idle");
        }
        if (this.anim.GetBool("Shoot"))
            this.anim.Play("Shoot");
    }
    //void Update()
    //{
    //    anim = model.GetComponent<Animator>();

    //    if (Joystick_test == true)
    //    {
    //        this.transform.LookAt(target);
    //        if (joystick.Vertical != 0 || joystick.Horizontal != 0)
    //        {
    //            this.anim.Play("Run");
    //            this.transform.Translate(new Vector3(joystick.Direction.x, 0, joystick.Direction.y) * this.speed * Time.deltaTime, Space.World);
    //        }
    //        else
    //            this.anim.Play("Idle");
    //    }
    //    else if (Joystick_test == false)
    //    {
    //        if (joystick.Vertical != 0 || joystick.Horizontal != 0)
    //        {
    //            this.anim.Play("Run");
    //            this.transform.Translate(Vector3.forward * this.speed * Time.deltaTime);
    //            this.transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg, 0f);
    //        }
    //        else
    //            this.anim.Play("Idle");
    //    }

    //    //if (this.anim.GetBool("Shoot"))
    //    //    this.anim.Play("Shoot");

    //    Debug.LogFormat($" GetBool : {this.anim.GetBool("Shoot")}");
    //    //if (Input.GetKeyUp(KeyCode.Space))
    //    //{
    //    //    this.anim.SetBool("Shoot", false);
    //    //    Debug.Log("Space Click Up!!!");
    //    //}
    //    //else if (Input.GetKeyDown(KeyCode.Space))
    //    //{
    //    //    this.anim.SetBool("Shoot", true);
    //    //    Debug.Log("Space Click Down!!!");
    //    //}
    //    //if (this.anim.GetBool("Shoot"))
    //    //    this.transform.LookAt(target);
    //    //else
    //    //    this.target = null;
    //}
    private void SetPlayerState(ePlayerState state)
    {
        eState = state;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            this.anim.SetBool("Shoot", true);
            this.target = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        this.anim.SetBool("Shoot", false);
    }
}
