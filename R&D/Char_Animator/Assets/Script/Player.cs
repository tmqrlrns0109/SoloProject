using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        // Collider Test==========
        Collider[] collisions = Physics.OverlapSphere(this.transform.position, 5f);
        foreach(Collider coll in collisions)
        {
            if(coll.gameObject.tag == "Enemy")
            {
                Debug.LogFormat($"Enermy GameObject Name : {coll.gameObject.name}");

                this.anim.SetBool("Shoot", true);
                this.target = coll.transform;
                break;
            }
        }
        //=========================
        

        if (this.anim.GetBool("Shoot") == true)
        {
            this.transform.LookAt(target);
            
            this.model.transform.localRotation = Quaternion.Euler(0, 40, 0);        // 사격 애니메이션 초점 맞추기 위해
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
            this.model.transform.localRotation = Quaternion.Euler(0, 0, 0);        // 사격 애니메이션 끝난 후 다시 원상복구 시키기 위해

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
        else if(other.tag == "Item")
        {
            other.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        this.anim.SetBool("Shoot", false);
    }
}
