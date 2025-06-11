using UnityEngine;
using System.Collections.Generic;

public class HexMapNoGap : MonoBehaviour
{
    [Header("Hex Prefab / Map Settings")]
    public GameObject hexPrefab;
    public int width = 7;
    public int height = 7;

    [Header("Iso Angle")]
    [Tooltip("타일 평면을 X축 기준으로 기울일 각도")]
    public float tiltAngleX = 60f;
    Quaternion tileRotation;

    // 런타임에 검사해서 계산할 변수들
    float spriteWidth;   // 헥스 스프라이트의 월드 단위 너비 (지름)
    float spriteHeight;  // 헥스 스프라이트의 월드 단위 높이
    float horizStep;     // 헥스 간 가로 중심-중심 거리
    float vertStep;      // 헥스 간 세로 중심-중심 거리

    void Awake()
    {
        // 1) 스프라이트 렌더러에서 실제 크기 읽기
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
        // 3) Axial 좌표 → World 위치로 Instantiate
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
