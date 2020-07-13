using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    private Dictionary<int, WeaponTrData> dicWeaponTrDates;


    private void Start()
    {
        this.dicWeaponTrDates = new Dictionary<int, WeaponTrData>();
    }
    public static DataManager GetInstance()
    {
        if (DataManager.instance == null)
            DataManager.instance = new DataManager();
        return DataManager.instance;
    }
    public void LoadAllDatas()
    {
        TextAsset WeaponTrTextAsset = Resources.Load<TextAsset>("Data/Json/WeaponTrData");
        string WeaponTrJson = WeaponTrTextAsset.text;
        var arrWeaponTrData = JsonConvert.DeserializeObject<WeaponTrData[]>(WeaponTrJson);
        this.dicWeaponTrDates = arrWeaponTrData.ToDictionary(x => x.id);
    }
    public List<WeaponTrData> GetWeaponTrDatas()
    {
        return this.dicWeaponTrDates.Values.ToList();
    }
}
