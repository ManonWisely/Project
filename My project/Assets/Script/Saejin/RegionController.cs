using System.Collections.Generic;
using UnityEngine;

public class RegionController : MonoBehaviour
{
    [Tooltip("이 영역이 클리어되었는지")]
    public bool isCleared = false;
    [Tooltip("이 영역과 인접한(해제할) 이웃 영역들")]
    public List<RegionController> neighbors;
}
