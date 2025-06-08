using UnityEngine;
using UnityEngine.SceneManagement;

public class GridPersistence : MonoBehaviour
{
    public static GridPersistence Instance;

    [Tooltip("Grid를 활성화할 씬 이름 (필드 씬 등)")]
    public string activeOnlyInScene = "FelixFieldScene";

    void Awake()
    {
        // 싱글톤 중복 방지
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // 씬 전환 시 파괴되지 않도록 설정
        DontDestroyOnLoad(gameObject);

        // 씬이 로드된 직후에 콜백 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 로드된 직후에 실행됩니다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // activeOnlyInScene 이름과 일치하면 켜고, 아니면 끈다
        bool shouldBeActive = (scene.name == activeOnlyInScene);
        gameObject.SetActive(shouldBeActive);
    }
}
