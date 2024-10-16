using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponPickup : IInteractable 
{
    [SerializeField] private PlayerMenager.Weapons pickedWeapon;
    public override void Interact()
    {
        PickUp();
    }

    private void PickUp()
    {
        FindObjectOfType<PlayerMenager>().SetWeapon(pickedWeapon);
        gameObject.SetActive(false);
    }
}
