using UnityEngine;

[CreateAssetMenu(fileName = "NewAnima", menuName = "Corridor/Anima")]
public class CorridorEntry : ScriptableObject 
{
    public string animaName;
    public Sprite colorImage;
    //public Sprite sillhouetteImage;  //������ ���뵵
    public string description;

}
