using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private Joystick joystick;
    [SerializeField]
    private Animator anim;
    public GameObject model;
    public float speed;


    // Test Code
    private Transform target;
    private DataManager dataManager;
    
    [SerializeField]
    private GameObject itemColliderGo;
    [SerializeField]
    private GetItemCollider itemCollider;
    [SerializeField]
    private GameObject equipItem;


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
        dataManager = DataManager.GetInstance();
        dataManager.LoadAllDatas();

        joystick = FindObjectOfType<Joystick>();

        this.dataManager = DataManager.GetInstance();
        this.dataManager.LoadAllDatas();

        this.itemCollider = this.itemColliderGo.GetComponent<GetItemCollider>();
        this.itemCollider.getItem = (item) =>
        {
            this.equipItem = item;
            if(this.equipItem != null)
            {
                Transform weaponPivot = GameObject.Find("WeaponPivot").transform;
                //Debug.LogFormat($"Weapon Pivot : {weaponPivot}");
                if(weaponPivot.childCount != 0) // 만약 기존에 무기를 장착하고 있었다면
                {
                    Destroy(weaponPivot.GetChild(0).gameObject);    // 현재 무기 제거
                    
                    this.equipItem.transform.SetParent(weaponPivot, true);  // 습득한 무기를 Pivot에 부착
                    this.equipItem.SetActive(true);         // 무기 SetActive

                    this.SetWeaponTr(item.name);            // DataManager에서 읽어온 WeaponTrData 대로 Transform 조정 
                }
            }
        };
    }
    public void SetWeaponTr(string weaponName)
    {
        // Data Manager에서 Weapon Data 골라내기
        var weaponData = dataManager.GetWeaponTrDatas();
        foreach (WeaponTrData data in weaponData)
        {
            if (data.res_name == weaponName)
            {
                this.equipItem.transform.localPosition = new Vector3(data.pos_x, data.pos_y, data.pos_z);
                this.equipItem.transform.localRotation = Quaternion.Euler(data.rot_x, data.rot_y, data.rot_z);
            }
        }

    }
    private void Update()
    {
        anim = model.GetComponent<Animator>();

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
        //else if (other.tag == "item")
        //{
        //    other.gameobject.setactive(false);
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        this.anim.SetBool("Shoot", false);
    }
}
