using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static AnimaEntry;

public class AnimaUI : MonoBehaviour
{
    [Header("���� ���� ����")]
    [SerializeField] private Transform gridParent;
    [SerializeField] private GameObject slotPrefab;

    [Header("�� ���� �г�")]
    [SerializeField] private AnimaDetailUI detailPanel;

    [Header("�� ��ư (��ü + ���� 8��)")]
    [SerializeField] private Toggle[] emotionToggles;

    private EmotionType? currentFilter = null;
    private void OnEnable()
    {
        SetupTabs();
        Refresh();
    }
    private void SetupTabs()
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

                    for (int j = 0; j < emotionToggles.Length; j++)
                        emotionToggles[j].interactable = j != index;
                }
            });
        }
    }
    private void Refresh()
    {
        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        List<AnimaEntry> animaList = currentFilter == null
            ? CorridorManager.Instance.GetAllAnima()
            : CorridorManager.Instance.GetByEmotion(currentFilter.Value);

        foreach (var anima in animaList)
        {
            bool discovered = CorridorManager.Instance.IsDiscovered(anima);

            if (currentFilter != null && !discovered)
                continue;

            GameObject obj = Instantiate(slotPrefab, gridParent);
            AnimaSlot slot = obj.GetComponent<AnimaSlot>();
            slot.Setup(anima, discovered);
            slot.onClick.AddListener(ShowDetail);
        }

        var first = animaList.Find(a => currentFilter == null || CorridorManager.Instance.IsDiscovered(a));
        if (first != null) ShowDetail(first);
    }
    private void ShowDetail(AnimaEntry anima)
    {
        bool discovered = CorridorManager.Instance.IsDiscovered(anima);
        detailPanel.Display(anima, discovered);
    }
    public void OnBackButton()
    {
        gameObject.SetActive(false);
    }
}