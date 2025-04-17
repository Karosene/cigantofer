using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGun : MonoBehaviour
{
    public GameObject gunPrefab;  // Reference to the gun prefab to spawn when dropped
    private bool hasGunEquipped = true; // Indicates if the player currently has the gun equipped

    void Update()
    {
        // Drop the weapon when "G" key is pressed
        if (Input.GetKeyDown(KeyCode.G) && hasGunEquipped)
        {
            DropWeapon();
        }
    }

    private void DropWeapon()
    {
        // Check if gunPrefab is set
        if (gunPrefab == null)
        {
            Debug.LogWarning("Gun prefab is not assigned!");
            return;
        }

        // Set the drop position a bit in front of the player
        Vector3 dropPosition = transform.position + transform.forward * 1.5f;

        // Instantiate the gun at the drop position with no rotation
        GameObject droppedGun = Instantiate(gunPrefab, dropPosition, Quaternion.identity);

        // Apply a small force to make it look like it’s "falling" or "tossed" from the player
        Rigidbody rb = droppedGun.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * 2f + transform.up * 2f, ForceMode.Impulse);
        }

        // Set hasGunEquipped to false so it can only be dropped once
        hasGunEquipped = false;
    }
}
