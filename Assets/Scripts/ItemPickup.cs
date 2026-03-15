using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // Reference to the outline component on this item
    private Outline outline;

    public string itemName;


    private void Start()
    {
        // Get the outline component on this object
        outline = GetComponent<Outline>();

        // Turn off outline at the start
        if (outline != null)
        {
            outline.enabled = false;
        }
    }

    // Turn highlight ON
    public void Highlight()
    {
        if (outline != null)
        {
            outline.enabled = true;
        }
    }

    // Turn highlight OFF
    public void Unhighlight()
    {
        if (outline != null)
        {
            outline.enabled = false;
        }
    }

    // Collect this item
    public void Collect()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CollectItem(itemName);
            GameManager.Instance.HidePrompt();
        }

        Destroy(gameObject);
    }
}   