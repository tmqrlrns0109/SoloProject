using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetItemCollider : MonoBehaviour
{
    public GameObject item;

    // UnityAction Test Code
    public UnityAction<GameObject> getItem;
    private Player player;

    /// <summary>
    /// 아이템 획득 이벤트
    /// </summary>
    /// <param name="other">획득한 아이템의 콜라이더</param>
    private void OnTriggerStay(Collider other)
    {        
        if (other.tag == "Item")
        {
            Debug.LogFormat($"Get Item : {other.name}");
            this.item = other.gameObject;
            other.gameObject.SetActive(false);
            getItem(other.gameObject);
        }
    }
}
