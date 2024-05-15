using UnityEngine;
using TMPro; // Add this line

public class DeadMeat : MonoBehaviour
{
    public TextMeshProUGUI gameTimeText;

    void Start()
    {
        // Display the game time in the UI
        gameTimeText.text = "Time: " + GameData.GameTime.ToString("F2");
    }
}

