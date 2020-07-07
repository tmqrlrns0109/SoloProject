using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public GameObject gunPrefab;
    public Transform handTrans;
    public bool worldPositionStay;
    // Start is called before the first frame update
    void Start()
    {
        var gunGo = Instantiate(gunPrefab);
        gunGo.transform.SetParent(this.handTrans, worldPositionStay);
        gunGo.transform.localPosition = new Vector3(0.237f, 0.121f, -0.216f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
