using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BoostPotion", menuName = "Items/BoostPotion", order = 3)]

public class BoostConsumableScriptable : ConsumableScriptable
{
    public override void UseItem(PlayerController playerController)
    {

        if (playerController.movementComponent.isUsingBoost) return;

        playerController.movementComponent.walkSpeed *= 2;
        playerController.movementComponent.runSpeed *= 2;

        playerController.movementComponent.boostTimer = 10;
        playerController.movementComponent.isUsingBoost = true;
        base.UseItem(playerController);
    }
}
