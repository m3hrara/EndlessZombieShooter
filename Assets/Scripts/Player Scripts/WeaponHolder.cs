using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    // public AmmoUI ammoUI;

    [Header("WeaponToSpawn"), SerializeField]
    GameObject weaponToSpawn;

    public PlayerController playerController;
    Animator animator;
    Sprite crosshairImage;
    public WeaponComponent equippedWeapon;

    [SerializeField]
    GameObject weaponsSocketLocation;
    [SerializeField]
    Transform gripIKSocketLocation;

    bool wasFiring = false;
    bool firingPressed = false;
    GameObject spawnedWeapon;
    public WeaponScriptable startingWeaponScriptable;
    // Start is called before the first frame update
    public readonly int isFiringHash = Animator.StringToHash("IsFiring");
    public readonly int isReloadingHash = Animator.StringToHash("IsReloading");
    public Dictionary<WeaponType, WeaponStats> WeaponAmmoData;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        WeaponAmmoData = new Dictionary<WeaponType, WeaponStats>();
        playerController.inventory.AddItem(startingWeaponScriptable, 1);
        WeaponAmmoData.Add(startingWeaponScriptable.weaponStats.weapontype, startingWeaponScriptable.weaponStats);
        //EquipWeapon(startingWeaponScriptable);
        //spawnedWeapon = Instantiate(weaponToSpawn, weaponsSocketLocation.transform.position, weaponsSocketLocation.transform.rotation, weaponsSocketLocation.transform);
        //startingWeaponScriptable.UseItem(playerController);
        //equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        //equippedWeapon.Initialize(this, startingWeaponScriptable);
        //PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(equippedWeapon)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, gripIKSocketLocation.transform.position);
        }
    }

    public void OnFire(InputValue value)
    {
        firingPressed = value.isPressed;
        if(!equippedWeapon)
        {
            return;
        }
        if (firingPressed)
        {
            StartFiring();
        }
        else
        {
            StopFiring();
        }
    }

    public void StartFiring()
    {
        if (equippedWeapon.weaponStats.bulletsInClip <= 0)
        {
            StartReloading();
            return; 
        }
        animator.SetBool(isFiringHash, true);
        playerController.isFiring = true;
        equippedWeapon.StartFiringWeapon();
    }

    public void StopFiring()
    {
        playerController.isFiring = false;
        animator.SetBool(isFiringHash, false);
        equippedWeapon.StopFiringWeapon();
    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        if (!equippedWeapon)
        {
            return;
        }
        StartReloading();
    }

    public void StartReloading()
    {
        if (equippedWeapon.isReloading || equippedWeapon.weaponStats.bulletsInClip == equippedWeapon.weaponStats.clipSize) return;

        if (playerController.isFiring)
        {
            StopFiring();
        }
        if (equippedWeapon.weaponStats.totalBullets <= 0) return;
        animator.SetBool(isReloadingHash, true);
        equippedWeapon.StartReloading();
        WeaponAmmoData[equippedWeapon.weaponStats.weapontype] = equippedWeapon.weaponStats;
        InvokeRepeating(nameof(StopReloading), 0, 0.1f);

    }
    public void StopReloading()
    {
        if (animator.GetBool(isReloadingHash)) return;
        playerController.isReloading = false;
        equippedWeapon.StopReloading();
        animator.SetBool(isReloadingHash, false);
        CancelInvoke(nameof(StopReloading));

    }
    public void EquipWeapon(WeaponScriptable weaponScriptable)
    {
        if (!weaponScriptable) return;
        spawnedWeapon = Instantiate(weaponScriptable.itemPrefab, weaponsSocketLocation.transform.position, weaponsSocketLocation.transform.rotation, weaponsSocketLocation.transform);
        if (!spawnedWeapon) return;

        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        if (!equippedWeapon) return;

        equippedWeapon.Initialize(this, weaponScriptable);

        if (WeaponAmmoData.ContainsKey(equippedWeapon.weaponStats.weapontype))
        {
            equippedWeapon.weaponStats = WeaponAmmoData[equippedWeapon.weaponStats.weapontype];
        }

        PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);
        gripIKSocketLocation = equippedWeapon.gripLocation;
        //ammoUI.OnWeaponEquipped(equippedWeapon);
    }
    public void UnequipWeapon()
    {
        if(!equippedWeapon)
        {
            return;
        }
        if (WeaponAmmoData.ContainsKey(equippedWeapon.weaponStats.weapontype))
        {
            WeaponAmmoData[equippedWeapon.weaponStats.weapontype] = equippedWeapon.weaponStats;
        }
        Destroy(equippedWeapon.gameObject);
        equippedWeapon = null;
    }
}