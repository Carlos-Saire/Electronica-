using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Register : MonoBehaviour
{
    [Header("Card Registration")]
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_Text uidText;
    [SerializeField] private Button startGameButton;
    private void GetCardId(string value)
    {
        uidText.text = value;
    }
    private void OnEnable()
    {
        ArduinoObserver.OnCardId += GetCardId;
    }
    private void OnDisable()
    {
        ArduinoObserver.OnCardId -= GetCardId;
    }
}

[System.Serializable]
public class CardData
{
    public string name;
    public string cardUID;
}
