using System.Collections.Generic;
using UnityEngine;

public class RegionController : MonoBehaviour
{
    [Tooltip("�� ������ Ŭ����Ǿ�����")]
    public bool isCleared = false;
    [Tooltip("�� ������ ������(������) �̿� ������")]
    public List<RegionController> neighbors;
}
