using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponsDatainfo", menuName = "WeaponsSaveData/WeaponsSaveDataObject", order = 1)]


public class Weaponsinfo : ScriptableObject
{
    public List<Weapons> weaponsList = new List<Weapons>();
}



[System.Serializable]
public class Weapons
{
    public GameObject weaponObject;
    public string weaponName;
    public float weaponDamagae;
    public float numOfUse;
}
