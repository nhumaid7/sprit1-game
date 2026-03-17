using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI counterText;
    public GameObject pickupPrompt;
    public TextMeshProUGUI shoppingListText;

    [Header("All possible items in the store")]
    public List<string> allPossibleItems = new List<string>
    {
        "Cereal",
        "Milk",
        "Salt",
        "Olive Oil",
        "Strawberry",
        "Croissant",
        "Bread",
        "Honey",
        "Peanut butter",
        "Water",
        "Nachos",
        "Musterd",
        "Ketchup",
        "Eggplant",
        "Paprika",
        "Round loaf",
        "Cookie",
        "Cupcake",
        "Chocolate"
    };

    [Header("How many items to choose each round")]
    public int itemsPerRound = 6;

    // Keep this public so old scripts that use requiredItems still work
    public List<string> requiredItems = new List<string>();

    private HashSet<string> collectedCorrectItems = new HashSet<string>();

    public int TotalRequiredItems => requiredItems.Count;
    public int CollectedCorrectCount => collectedCorrectItems.Count;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartNewRound();
        UpdateShoppingListUI();
        UpdateCounter();

        if (pickupPrompt != null)
        {
            pickupPrompt.SetActive(false);
        }
    }

    public void StartNewRound()
    {
        collectedCorrectItems.Clear();
        requiredItems = GetRandomItems(allPossibleItems, itemsPerRound);

        UpdateShoppingListUI();
        UpdateCounter();
    }

    private List<string> GetRandomItems(List<string> sourceList, int count)
    {
        List<string> tempList = new List<string>(sourceList);
        List<string> randomItems = new List<string>();

        count = Mathf.Min(count, tempList.Count);

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            randomItems.Add(tempList[randomIndex]);
            tempList.RemoveAt(randomIndex);
        }

        return randomItems;
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

    public bool IsRequiredItem(string itemName)
    {
        return requiredItems.Contains(itemName.Trim());
    }

    public bool IsAlreadyCollected(string itemName)
    {
        return collectedCorrectItems.Contains(itemName.Trim());
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

    private void UpdateShoppingListUI()
    {
        if (shoppingListText == null) return;

        string text = "";

        foreach (string item in requiredItems)
        {
            if (collectedCorrectItems.Contains(item))
            {
                text += "[X] " + item + "\n";
            }
            else
            {
                text += "[ ] " + item + "\n";
            }
        }

        shoppingListText.text = text;
    }
}