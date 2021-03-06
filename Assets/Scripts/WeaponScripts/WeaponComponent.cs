using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None, Pistol, MachineGun
}

public enum WeaponFiringPattern
{
    SemiAuto, FullAuto, ThreeShotBurst, FiveShotBurst
}


[System.Serializable]
public struct WeaponStats
{
    public WeaponType weapontype;
    public string weaponName;
    public float damage;
    public int bulletsInClip;
    public int clipSize;
    public float fireStartDelay;
    public float fireRate;
    public WeaponFiringPattern weaponFiringPattern;
    public float fireDistance;
    public bool repeating;
    public LayerMask weaponHitLayers;
    public int totalBullets;
}

public class WeaponComponent : MonoBehaviour
{
    public Transform gripLocation;
    public Transform firingEffectLocation;
    protected WeaponHolder weaponHolder;

    [SerializeField]
    public WeaponStats weaponStats;

    [SerializeField]
    protected ParticleSystem firingEffect;
    public WeaponFiringPattern pattern;
    public bool isFiring = false;
    public bool isReloading = false;
    protected Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void Initialize(WeaponHolder _weaponHolder, WeaponScriptable weaponScriptable)
    {
        weaponHolder = _weaponHolder;
        if(weaponScriptable)
        {
            weaponStats = weaponScriptable.weaponStats;
        }
    }

    //decide weather auto or single fire
    public virtual void StartFiringWeapon()
    {
        isFiring = true;
        if (weaponStats.repeating)
        {
            InvokeRepeating(nameof(FireWeapon), weaponStats.fireStartDelay, weaponStats.fireRate);
        }
        else
        {
            FireWeapon();
        }
    }

    public virtual void StopFiringWeapon()
    {
        isFiring = false;
        CancelInvoke(nameof(FireWeapon));

        if (firingEffect.isPlaying)
        {
            firingEffect.Stop();
        }
        print("stop firing!");
    }

    protected virtual void FireWeapon()
    {
        print("firing weapon!");
        weaponStats.bulletsInClip--;
        if (firingEffect)
        {
            firingEffect.Play();
        }
    }
    public virtual void StartReloading()
    {
        isReloading = true;
        ReloeadWeapon();
    }
    public virtual void StopReloading()
    {
        isReloading = false;
    }
    protected virtual void ReloeadWeapon()
    {
        if (firingEffect.isPlaying)
        {
            firingEffect.Stop();
        }
        int bulletsToReload = weaponStats.totalBullets - (weaponStats.clipSize - weaponStats.bulletsInClip);
        if (bulletsToReload > 0)
        {
            weaponStats.totalBullets = bulletsToReload;
            weaponStats.bulletsInClip = weaponStats.clipSize;
        }
        else
        {
            weaponStats.bulletsInClip += weaponStats.totalBullets;
            weaponStats.totalBullets = 0;
        }
    }
}