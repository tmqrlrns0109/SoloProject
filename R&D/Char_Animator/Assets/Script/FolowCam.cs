using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowCam : MonoBehaviour
{
    //추적할 대상
    public Transform target;
    //카메라와의 거리   
    public float dist = 7f;
    //카메라의 높이 
    public float height = 5f;


    private Transform tr;

    void Start()
    {
        // 시작과 동시에 Tag : Player를 찾아서 Target 설정 후 Trace
        this.target = FindObjectOfType<Player>().transform;
        tr = GetComponent<Transform>();
    }
 
    private void LateUpdate()
    {
        //카메라 위치 설정
        tr.position = target.position - (1 * Vector3.forward * dist) + (Vector3.up * height);
        tr.LookAt(target);
    }

}
