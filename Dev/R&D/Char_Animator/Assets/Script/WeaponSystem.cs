using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public List<GameObject> listWeapon;
    public GameObject player;
    public float speed = 300;

    private void Start()
    {
        var weaponStartPosition = 1;
        for(int idx = 0; idx<listWeapon.Count;idx++)
        {
            listWeapon[idx].transform.localPosition = new Vector3(weaponStartPosition++, 0, 0);
        }
    }
    void Update()
    {
        for (int idx = 0; idx < listWeapon.Count; idx++)
        {
            this.OrbitAround(listWeapon[idx], this.speed+(50*idx));
            this.Orbit_Rotation(listWeapon[idx], this.speed);
        }
    }

    private void OrbitAround(GameObject weapon, float speed)       // 공전
    {
        weapon.transform.RotateAround(player.transform.position, Vector3.down, speed * Time.deltaTime);
    }
    private void Orbit_Rotation(GameObject weapon, float speed)     // 자전
    {
        weapon.transform.Rotate(new Vector3(5f, 0f, 0f) * speed * Time.deltaTime);
    }
}
