using UnityEngine;
using DG.Tweening;

public class a : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.DOMove(new Vector3(5, 0, 0), 5);
    }
}
