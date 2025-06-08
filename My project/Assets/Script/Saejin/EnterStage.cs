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
            Debug.LogError("sceneToLoad 가 비어 있습니다!");
            return;
        }
        if (stageNode.stat == "locked")
        {
            Debug.Log("잠겨있습니다.");
            return;
        }
        else if (stageNode.stat == "cleared")
        {
            Debug.Log("클리어 상태입니다.");
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
