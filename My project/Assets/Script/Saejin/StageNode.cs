using System.Collections.Generic;
using UnityEngine;

public class StageNode : MonoBehaviour
{
    public bool isSelected = false;
    public bool isLast = false;
    public List<StageNode> prevNodes = new List<StageNode>();
    public List<StageNode> nextNodes = new List<StageNode>();
    public int idx;
    public int prevNodeCount = 0;
    public int type = 0; // �� Ÿ���� 0 ~ 6 �� 7�� �����ϰ� ����
    public string stat;
}
