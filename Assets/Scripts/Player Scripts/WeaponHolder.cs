using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]
    GameObject weaponToSpawn;

    PlayerController playerController;
    Sprite crosshairImage;

    [SerializeField]
    GameObject weaponsSocketLocation;
    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnedWeapon = Instantiate(weaponToSpawn, weaponsSocketLocation.transform.position, weaponsSocketLocation.transform.rotation, weaponsSocketLocation.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
}