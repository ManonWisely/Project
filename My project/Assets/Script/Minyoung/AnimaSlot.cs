using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class AnimaSlot : MonoBehaviour
{
    [Header("UI ���")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Button slotButton;

    private AnimaEntry animaData;

    public UnityEvent<AnimaEntry> onClick = new();

    public void Setup(AnimaEntry entry, bool isDiscovered)
    {
        animaData = entry;

        Sprite sprite = entry.GetImage();
        iconImage.sprite = sprite;
        iconImage.color = isDiscovered ? Color.white : Color.black;

        nameText.text = isDiscovered ? entry.name : "???";

        slotButton.onClick.RemoveAllListeners();
        slotButton.onClick.AddListener(() => onClick.Invoke(animaData));
    }
}