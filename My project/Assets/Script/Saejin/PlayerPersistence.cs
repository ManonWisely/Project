using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPersistence : MonoBehaviour
{
    public static PlayerPersistence Instance;

    [Tooltip("�� �������� Player�� Ȱ��ȭ�ϰ� �ʹٸ� ���⿡ �� �̸��� ����μ���.")]
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

        // ���� �ε�� ���Ŀ� ȣ��� �ݹ� ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // �޸� ���� ������ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ���� �ε�� ���Ŀ� ����˴ϴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���ϴ� �� �̸��� ��ġ�� ���� Ȱ��ȭ, �ƴϸ� ��Ȱ��ȭ
        bool shouldBeActive = (scene.name == activeOnlyInScene);
        gameObject.SetActive(shouldBeActive);
    }
}
