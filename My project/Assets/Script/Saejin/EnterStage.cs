using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class EnterStage : MonoBehaviour, IPointerClickHandler
{
    public string sceneToLoad;
    private List<string> stageNames = new List<string> { "FelixFieldScene", "PhobiaFieldScene", "OdiumFieldScene", "AmareFieldScene", "IrascorFieldScene", "LacrimaFieldScene", "HavetFieldScene" };
    private StageNode stageNode;
    private SpawnStage spawnStage;

    public void OnPointerClick(PointerEventData eventData)
    {
        stageNode = GetComponent<StageNode>();
        spawnStage = GetComponentInParent<SpawnStage>();
        sceneToLoad = stageNames[stageNode.type];
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogError("sceneToLoad �� ��� �ֽ��ϴ�!");
            return;
        }
        if (stageNode.stat == "locked")
        {
            Debug.Log("����ֽ��ϴ�.");
            return;
        }
        else if (stageNode.stat == "cleared")
        {
            Debug.Log("Ŭ���� �����Դϴ�.");
            return;
        }
        else
        {
            for (int i = 0; i < stageNode.nextNodes.Count; i++)
            {
                stageNode.nextNodes[i].stat = "opened";
                stageNode.stat = "cleared";
                spawnStage.find_section(stageNode);
            }
            //SceneManager.LoadScene(sceneToLoad);
        }
    }
}
