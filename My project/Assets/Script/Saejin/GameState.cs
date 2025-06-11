using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    // �� �� �̸��� ����
    [HideInInspector] public string mapSceneName = "FelixFieldScene";
    // regionId�� Ŭ���� ���¸� ����
    public Dictionary<string, bool> clearedRegions = new Dictionary<string, bool>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}