using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CorridorManager : MonoBehaviour
{
    public static CorridorManager Instance { get; private set; }

    [Header("��ü �ƴϸ� �����ͺ��̽�")]
    public List<AnimaEntry> animaDatabase = new();  // BGDatabase���� �ε�� ������

    // �߰� ����: name ����, 0 = �̹߰�, 1 = �߰�
    private Dictionary<string, int> meetStatusMap = new();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init(); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Init()
    {
        animaDatabase = AnimaEntry.LoadAll();

        // �߰� ���� �ҷ����� (������ �ӽ�)
        LoadMeetedData();

        // ����: ������ �ϳ� �߰� ó�� (�׽�Ʈ��)
        if (animaDatabase.Count > 0)
        {
            MarkDiscovered(animaDatabase[0]);
        }
    }

    public bool IsDiscovered(AnimaEntry entry)
    {
        return meetStatusMap.TryGetValue(entry.name, out int value) && value >= 1;
    }

    public void MarkDiscovered(AnimaEntry entry)
    {
        if (!meetStatusMap.ContainsKey(entry.name) || meetStatusMap[entry.name] == 0)
        {
            meetStatusMap[entry.name] = 1;
            SaveMeetedData();  // ������ ���� ����
        }
    }

    public List<AnimaEntry> GetAllAnima()
    {
        return animaDatabase;
    }

    public List<AnimaEntry> GetByEmotion(EmotionType emotion)
    {
        return animaDatabase.Where(a => a.emotion == emotion).ToList();
    }

    // ���� ����: �߰� ���� ����
    private void SaveMeetedData()
    {
        // ��: PlayerPrefs �Ǵ� JSON
    }

    // ���� ����: �߰� ���� �ҷ�����
    private void LoadMeetedData()
    {
        // ��: PlayerPrefs �Ǵ� JSON
        // �ʱ�ȭ �� ��� meet ���¸� 0���� ����
        foreach (var anima in animaDatabase)
        {
            if (!meetStatusMap.ContainsKey(anima.name))
                meetStatusMap[anima.name] = 0;
        }
    }
}
