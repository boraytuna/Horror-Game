using UnityEngine;

[CreateAssetMenu(fileName = "New Battery", menuName = "Inventory/Battery")]
public class Battery : Item
{
    public float chargeAmount;  // The amount of battery life this battery adds

    public override bool Use(GameObject user)
    {
        Flashlight flashlight = user.GetComponentInChildren<Flashlight>();
        if (flashlight == null)
        {
            Debug.Log("No flashlight found on the user.");
            return false;
        }

        return flashlight.Recharge(chargeAmount);
    }
}
