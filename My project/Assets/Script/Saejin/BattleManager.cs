using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    // �� ������ ���޹��� region �ĺ���
    public string regionId;

    // ���� ���� �� ȣ��
    public void OnBattleEnd()
    {
        // Ŭ���� ���� ����
        GameState.Instance.clearedRegions[regionId] = true;

        // �� ������ ��ε�
        SceneManager.LoadScene(GameState.Instance.mapSceneName, LoadSceneMode.Single);
    }
}