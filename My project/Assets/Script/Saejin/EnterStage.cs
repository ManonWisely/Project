using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TreeEditor;

public class EnterStage : MonoBehaviour, IPointerClickHandler
{
    public string sceneToLoad;
    private List<string> stageNames = new List<string> { "FelixFieldScene", "PhobiaFieldScene", "OdiumFieldScene", "AmareFieldScene", "IrascorFieldScene", "LacrimaFieldScene", "HavetFieldScene" };
    private StageNode stageNode;
    private StageNode prevNode;
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

            if (stageNode.prevNodes.Count != 0)
            {
                prevNode = stageNode.prevNodes[0];
                for (int i = 0; i < prevNode.lines.Count; i++)
                {
                    if (prevNode.lines[i].endPoint == stageNode.transform)
                    {
                        continue;
                    }
                    else
                    {
                        prevNode.lines[i].setInvalidColor();
                    }

                }
            }
            for (int i = 0; i < stageNode.lines.Count; i++)
            {
                stageNode.lines[i].setOpenColor();
            }
            //SceneManager.LoadScene(sceneToLoad);
        }
    }
}
