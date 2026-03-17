using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Camera playerCamera;
    public float interactDistance = 3f;

    private ItemPickup currentItem;

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    void Update()
    {
        if (playerCamera == null)
        {
            Debug.LogWarning("Player Camera is not assigned in PlayerInteract.");
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Raycast against everything, not only collectibles
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            ItemPickup item = hit.collider.GetComponent<ItemPickup>();

            if (item == null)
            {
                item = hit.collider.GetComponentInParent<ItemPickup>();
            }

            if (item != null)
            {
                if (currentItem != item)
                {
                    ClearCurrentItem();
                    currentItem = item;
                    currentItem.Highlight();
                }

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.ShowPrompt();
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentItem.Collect();
                    currentItem = null;
                }

                return;
            }
        }

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