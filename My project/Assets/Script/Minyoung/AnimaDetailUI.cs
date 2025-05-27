using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimaDetailUI : MonoBehaviour
{
    [Header("UI ����")]
    public Image animaImage;
    public TMP_Text nameText;
    public TMP_Text descriptionText;

    public void Display(AnimaEntry anima, bool discovered)
    {
        animaImage.sprite = anima.colorImage; // �Ƿ翧�� �ʿ��ϸ� ���� ó�� ����

        nameText.text = discovered ? anima.animaName : "???";
        descriptionText.text = discovered ? anima.description : "???";
    }
}