using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimaUI : MonoBehaviour
{
    [Header("���� ���� ����")]
    public Transform gridParent;       // ScrollView > Content
    public GameObject slotPrefab;      // AnimaSlot ������

    [Header("�� ���� �г�")]
    public AnimaDetailUI detailPanel;

    [Header("�� ��ư (��ü + ���� 8��)")]
    public Toggle[] emotionToggles;    // 0: ��ü, 1~8: ������

    private EmotionType? currentFilter = null;

    void OnEnable()
    {
        SetupTabs();
        Refresh();
    }

    void SetupTabs()
    {
        for (int i = 0; i < emotionToggles.Length; i++)
        {
            int index = i;
            emotionToggles[i].onValueChanged.AddListener((isOn) =>
            {
                if (isOn)
                {
                    currentFilter = (index == 0) ? (EmotionType?)null : (EmotionType)(index - 1);
                    Refresh();
                }
            });
        }
    }

    void Refresh()
    {
        // ���� ��� ����
        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        // ���� ���͸� or ��ü
        List<AnimaEntry> animaList = currentFilter == null
            ? CorridorManager.Instance.GetAllAnima()
            : CorridorManager.Instance.GetByEmotion(currentFilter.Value);

        foreach (var anima in animaList)
        {
            GameObject obj = Instantiate(slotPrefab, gridParent);
            AnimaSlot slot = obj.GetComponent<AnimaSlot>();

            bool discovered = CorridorManager.Instance.IsDiscovered(anima);
            slot.Setup(anima, discovered);
            slot.onClick.AddListener(ShowDetail);
        }

        // ù �׸� �ڵ� ǥ��
        if (animaList.Count > 0)
            ShowDetail(animaList[0]);
    }

    void ShowDetail(AnimaEntry anima)
    {
        bool discovered = CorridorManager.Instance.IsDiscovered(anima);
        detailPanel.Display(anima, discovered);
    }
}