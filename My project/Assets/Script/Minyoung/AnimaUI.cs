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

                    for (int j = 0; j < emotionToggles.Length; j++)
                    {
                        emotionToggles[j].interactable = (j != index);
                    }
                }
            });
        }
    }

    void Refresh()
    {
        // ���� ����
        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        // ������ ���͸� �� ��ü
        List<AnimaEntry> animaList = currentFilter == null
            ? CorridorManager.Instance.GetAllAnima()
            : CorridorManager.Instance.GetByEmotion(currentFilter.Value);

        foreach (var anima in animaList)
        {
            bool discovered = CorridorManager.Instance.IsDiscovered(anima);

            // �߰ߵ��� ���� �ִ� ���� ���Ϳ��� ����
            if (currentFilter != null && !discovered)
                continue;

            GameObject obj = Instantiate(slotPrefab, gridParent);
            AnimaSlot slot = obj.GetComponent<AnimaSlot>();
            slot.Setup(anima, discovered);
            slot.onClick.AddListener(ShowDetail);
        }

        // ù �׸� �ڵ� ǥ��
        if (gridParent.childCount > 0)
        {
            AnimaEntry firstAnima = animaList.Find(a => currentFilter == null || CorridorManager.Instance.IsDiscovered(a));
            if (firstAnima != null)
                ShowDetail(firstAnima);
        }
    }

    void ShowDetail(AnimaEntry anima)
    {
        bool discovered = CorridorManager.Instance.IsDiscovered(anima);
        detailPanel.Display(anima, discovered);
    }

    public void OnBackButton()
    {
        this.gameObject.SetActive(false);
    }
}