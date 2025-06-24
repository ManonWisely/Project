using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CorridorManager : MonoBehaviour
{
    public static CorridorManager Instance { get; private set; }

    public List<AnimaEntry> animaDatabase = new();  // BGDatabase���� �ε�� ������

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

        //// �׽�Ʈ��: �� 50���� ������ ��ó��
        //for (int i = 0; i < 50 && i < animaDatabase.Count; i++)
        //{
        //    animaDatabase[i].meeted = 1;  
        //}

        // LoadMeetedData();  ���߿� ����� meeted �ҷ�����
    }

    public bool IsDiscovered(AnimaEntry entry)
    {
        return entry.meeted >= 1;
    }

    public void MarkDiscovered(AnimaEntry entry)
    {
        if (entry.meeted < 1)
        {
            entry.meeted = 1;
            SaveMeetedData();  // ���߿� ���� ����
        }
    }

    public void MarkCollected(AnimaEntry entry)
    {
        if (entry.meeted < 2)
        {
            entry.meeted = 2;
            SaveMeetedData();  // ���߿� ���� ����
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

    private void SaveMeetedData()
    {
        // TODO: PlayerPrefs, JSON �� ���� ����
    }

    private void LoadMeetedData()
    {
        // TODO: BGDatabase�κ��� �Ǵ� �ܺ� ����ҿ��� meeted �ҷ�����
    }
}
