using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private GameObject BowPrefab;

    [SerializeField] private GameObject weaponLogic;
    [SerializeField] private GameObject Arrow;
    [SerializeField] private Transform ShotPoint;
    [SerializeField] private Transform CrossHair;
    [SerializeField] private LayerMask Shootables;
    [SerializeField] private Transform DebugTransform;

    private void Start()
    {
        if(swordPrefab!= null && BowPrefab !=null)
        {
            ToggleSword(true);
            ToggleBow(false);
        }
        

    }

    public void EnableWeapon()
    {
        weaponLogic.SetActive(true);
    }

    public void DisableWeapon()
    {
        weaponLogic.SetActive(false);
    }

    public void Shoot()
    {
        Vector3 crossHairWorldPos = Vector3.zero; 

        Ray ray = Camera.main.ScreenPointToRay(CrossHair.position);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, Shootables);

        if (hasHit)
        {
            
            DebugTransform.position = hitInfo.point;
            Debug.Log("Hit " + hitInfo.collider.gameObject.name);
            crossHairWorldPos = hitInfo.point;
        }

        Vector3 aimDir = (crossHairWorldPos - ShotPoint.position).normalized;

        GameObject ar = Instantiate(Arrow, ShotPoint.position, Quaternion.LookRotation(aimDir,Vector3.up ));
        
    }

    public void ToggleSword(bool value)
    {
        swordPrefab?.SetActive(value);
    }
    public void ToggleBow(bool value)
    {
        BowPrefab?.SetActive(value);
    }
}
