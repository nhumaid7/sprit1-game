using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI counterText;
    public GameObject pickupPrompt;

    public TextMeshProUGUI shoppingListText;


    public List<string> requiredItems = new List<string>
    {
        "Cereal",
        "Milk",
        "Salt",
        "Olive Oil",
        "Strawberry",
        "Croissant"
    };

    private HashSet<string> collectedCorrectItems = new HashSet<string>();

    public int TotalRequiredItems => requiredItems.Count;
    public int CollectedCorrectCount => collectedCorrectItems.Count;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateShoppingListUI(); 
        UpdateCounter();

        if (pickupPrompt != null)
        {
            pickupPrompt.SetActive(false);
        }
    }

    public void CollectItem(string itemName)
    {
        itemName = itemName.Trim();

        if (requiredItems.Contains(itemName) && !collectedCorrectItems.Contains(itemName))
        {
            collectedCorrectItems.Add(itemName);
            UpdateShoppingListUI();
            UpdateCounter();
        }
        else
        {
            Debug.Log("Collected but not counted: " + itemName);
        }
    }

    private void UpdateCounter()
    {
        if (counterText != null)
        {
            counterText.text = "Items: " + CollectedCorrectCount + " / " + TotalRequiredItems;
        }
    }

    public bool HasWon()
    {
        return CollectedCorrectCount == TotalRequiredItems;
    }

    public void ShowPrompt()
    {
        if (pickupPrompt != null)
        {
            pickupPrompt.SetActive(true);
        }
    }

    public void HidePrompt()
    {
        if (pickupPrompt != null)
        {
            pickupPrompt.SetActive(false);
        }
    }
    void UpdateShoppingListUI()
    {
        string text = "";

        foreach (string item in requiredItems)
        {
            if (collectedCorrectItems.Contains(item))
            {
                text += "[X]" + item + "\n";
            }
            else
            {
                text += "[ ]" + item + "\n";
            }
        }

        shoppingListText.text = text;
    }


}