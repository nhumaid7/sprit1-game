using UnityEngine;

public class OutlineHighlight : MonoBehaviour
{
    public Behaviour outline;   // reference to the outline component

    void Awake()
    {
        outline = GetComponent<Behaviour>();
        outline.enabled = false;
    }

    public void SetHighlighted(bool state)
    {
        outline.enabled = state;
    }
}