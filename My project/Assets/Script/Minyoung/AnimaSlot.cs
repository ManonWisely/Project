using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class AnimaSlot : MonoBehaviour
{
    [Header("UI ���")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;

    private AnimaEntry animaEntry;

    // Ŭ�� �̺�Ʈ: �ܺο��� ������ ��� ����
    public UnityEvent<AnimaEntry> onClick = new();

    // ���� �ʱ�ȭ
    public void Setup(AnimaEntry entry, bool isDiscovered)
    {
        animaEntry = entry;

        iconImage.sprite = entry.colorImage; // ���� �Ƿ翧 ó�� �߰� ����
        nameText.text = isDiscovered ? entry.animaName : "???";
    }

    // UI ��ư���� �̰� ����
    public void OnSlotClicked()
    {
        onClick.Invoke(animaEntry);
    }
}