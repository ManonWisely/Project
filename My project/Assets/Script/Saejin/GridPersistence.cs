using UnityEngine;
using UnityEngine.SceneManagement;

public class GridPersistence : MonoBehaviour
{
    public static GridPersistence Instance;

    [Tooltip("Grid�� Ȱ��ȭ�� �� �̸� (�ʵ� �� ��)")]
    public string activeOnlyInScene = "FelixFieldScene";

    void Awake()
    {
        // �̱��� �ߺ� ����
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // �� ��ȯ �� �ı����� �ʵ��� ����
        DontDestroyOnLoad(gameObject);

        // ���� �ε�� ���Ŀ� �ݹ� ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ���� �ε�� ���Ŀ� ����˴ϴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // activeOnlyInScene �̸��� ��ġ�ϸ� �Ѱ�, �ƴϸ� ����
        bool shouldBeActive = (scene.name == activeOnlyInScene);
        gameObject.SetActive(shouldBeActive);
    }
}
