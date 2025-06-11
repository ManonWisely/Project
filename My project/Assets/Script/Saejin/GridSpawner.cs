using UnityEngine;
using System.Collections.Generic;

public class HexMapNoGap : MonoBehaviour
{
    [Header("Hex Prefab / Map Settings")]
    public GameObject hexPrefab;
    public int width = 7;
    public int height = 7;

    [Header("Iso Angle")]
    [Tooltip("Ÿ�� ����� X�� �������� ����� ����")]
    public float tiltAngleX = 60f;
    Quaternion tileRotation;

    // ��Ÿ�ӿ� �˻��ؼ� ����� ������
    float spriteWidth;   // �� ��������Ʈ�� ���� ���� �ʺ� (����)
    float spriteHeight;  // �� ��������Ʈ�� ���� ���� ����
    float horizStep;     // �� �� ���� �߽�-�߽� �Ÿ�
    float vertStep;      // �� �� ���� �߽�-�߽� �Ÿ�

    void Awake()
    {
        // 1) ��������Ʈ ���������� ���� ũ�� �б�
        var sr = hexPrefab.GetComponent<SpriteRenderer>();
        spriteWidth = sr.sprite.bounds.size.x;
        spriteHeight = sr.sprite.bounds.size.y;

        float rad = tiltAngleX * Mathf.Deg2Rad;
        horizStep = spriteWidth * 0.75f;
        vertStep = spriteHeight * Mathf.Cos(rad);

        tileRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    void Start()
    {
        // 3) Axial ��ǥ �� World ��ġ�� Instantiate
        for (int q = 0; q < width; q++)
        {
            for (int r = 0; r < height; r++)
            {
                float x = horizStep * q;
                float y = vertStep * (r + q * 0.5f);
                Vector3 pos = new Vector3(x, y, 0f);

                Instantiate(hexPrefab, pos, tileRotation, transform);
            }
        }
    }
}
