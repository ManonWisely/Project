using System.Collections.Generic;
using UnityEngine;

public class RegionController : MonoBehaviour
{
    [Tooltip("Ŭ���� & ���� ����")]
    public bool isCleared = false;
    public bool isVillaged = false;
    [Tooltip("�� ������ ������ Ÿ�� ����")]
    public List<RegionController> neighbors;
}
