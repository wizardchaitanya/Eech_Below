using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    public int chargeAmount = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement echo = other.GetComponent<PlayerMovement>();

            if (echo != null)
            {
                echo.AddEchoCharge(chargeAmount);
            }

            Destroy(gameObject);
        }
    }
}
