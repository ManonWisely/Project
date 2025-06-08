using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPersistence : MonoBehaviour
{
    public static PlayerPersistence Instance;

    [Tooltip("이 씬에서만 Player를 활성화하고 싶다면 여기에 씬 이름을 적어두세요.")]
    public string activeOnlyInScene = "FelixFieldScene";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);

        // 씬이 로드된 직후에 호출될 콜백 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // 메모리 누수 방지로 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 로드된 직후에 실행됩니다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 원하는 씬 이름과 일치할 때만 활성화, 아니면 비활성화
        bool shouldBeActive = (scene.name == activeOnlyInScene);
        gameObject.SetActive(shouldBeActive);
    }
}
