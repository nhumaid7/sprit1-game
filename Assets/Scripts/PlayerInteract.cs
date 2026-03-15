using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Camera used to aim from the center of the screen
    public Camera playerCamera;

    // Maximum distance from which the player can collect items
    public float interactDistance = 3f;

    // Layer used for collectible items
    public LayerMask collectibleLayer;

    // Stores the currently looked-at item
    private ItemPickup currentItem;

    void Update()
    {
        // Create a ray from the center of the camera
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        RaycastHit hit;

        // Check if the ray hits a collectible item within the allowed distance
        if (Physics.Raycast(ray, out hit, interactDistance, collectibleLayer))
        {
            // Try to get the ItemPickup script from the hit object
            ItemPickup item = hit.collider.GetComponent<ItemPickup>();

            // If the script is not on the collider object, try parent
            if (item == null)
            {
                item = hit.collider.GetComponentInParent<ItemPickup>();
            }

            // If we found a valid collectible item
            if (item != null)
            {
                // If this is a new item, remove highlight from old one
                if (currentItem != item)
                {
                    ClearCurrentItem();
                    currentItem = item;
                    currentItem.Highlight();
                }

                // Show the pickup prompt
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.ShowPrompt();
                }

                // Collect item when player presses E
                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentItem.Collect();
                    currentItem = null;
                }

                return;
            }
        }

        // If nothing valid is hit, clear current item
        ClearCurrentItem();
    }

    void ClearCurrentItem()
    {
        if (currentItem != null)
        {
            currentItem.Unhighlight();
            currentItem = null;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.HidePrompt();
        }
    }
}