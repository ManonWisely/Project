using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    // 맵 씬 이름을 저장
    [HideInInspector] public string mapSceneName = "FelixFieldScene";
    // regionId별 클리어 상태를 저장
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