using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CorridorManager : MonoBehaviour
{
    public static CorridorManager Instance { get; private set; }

    [Header("��ü �ƴϸ� �����ͺ��̽�")]
    public List<AnimaEntry> animaDatabase;  // �����Ϳ��� �巡���� ScriptableObject��

    private HashSet<string> discoveredAnimaIds = new();  // �߰ߵ� animaId ���

    void Start()
    {
        // �׽�Ʈ��: ���߿� �����
        var target = animaDatabase.Find(x => x.animaId == "gaudium1");
        if (target != null)
            MarkDiscovered(target);
    }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // ���� LoadDiscoveredData(); �߰� ����
    }

    // ��� Anima ��ȯ
    public List<AnimaEntry> GetAllAnima()
    {
        return animaDatabase;
    }

    // ������ ����
    public List<AnimaEntry> GetByEmotion(EmotionType emotion)
    {
        return animaDatabase.Where(a => a.emotion == emotion).ToList();
    }

    // �߰� ���� ��ȸ
    public bool IsDiscovered(AnimaEntry entry)
    {
        return discoveredAnimaIds.Contains(entry.animaId);
    }

    // �߰� ó��
    public void MarkDiscovered(AnimaEntry entry)
    {
        if (!discoveredAnimaIds.Contains(entry.animaId))
        {
            discoveredAnimaIds.Add(entry.animaId);
            // ���� SaveDiscoveredData(); ���� ����
        }
    }

    // ����/�ҷ����� ����� ���߿� �߰��ص� �˴ϴ�
}