using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HorizontalScrollByWheel : MonoBehaviour, IScrollHandler
{
    public ScrollRect scrollRect;
    public float scrollSensitivity = 0.03f; // �� ���� ���̸� �� ������

    public void OnScroll(PointerEventData eventData)
    {
        // eventData.scrollDelta.y�� �� ��(+1), �Ʒ�(-1)
        scrollRect.horizontalNormalizedPosition -= eventData.scrollDelta.y * scrollSensitivity;
    }
}