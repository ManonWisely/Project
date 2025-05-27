using UnityEngine;

public enum EmotionType
{
    Wrath,    // �г� 
    Void,     // ���� 
    Love,     // ���
    Desire,   // ���  
    Joy,      // ��ſ� 
    Fear,     // �η���
    Sorrow,   // ����
    Gaudium   // ���
}

[CreateAssetMenu(fileName = "NewAnima", menuName = "Corridor/Anima Entry")]
public class AnimaEntry : ScriptableObject
{
    [Header("�⺻ ����")]
    public string animaId;              // ���� ID (����/����ȭ��)
    public string animaName;
    [TextArea]
    public string description;
    public EmotionType emotion;        // ���� ������ ���� �� ���͸��� ���

    [Header("�̹���")]
    public Sprite colorImage;          // �߰� �� ǥ�ÿ�
    public Sprite silhouetteImage;     // �߰� �� ǥ�ÿ� (��� �Ƿ翧)

    // Ȯ�� ����: ������, �нú�, �ִϸ��̼� �� �߰� ����
    // public int powerLevel;
    // public string passiveSkill;
}