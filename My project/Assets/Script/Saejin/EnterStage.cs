using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TreeEditor;

public class EnterStage : MonoBehaviour, IPointerClickHandler
{
    public string sceneToLoad;
    public string sceneToLoadBoss;
    private List<string> stageNames = new List<string> { "FelixFieldScene", "PhobiaFieldScene", "OdiumFieldScene", "AmareFieldScene", "IrascorFieldScene", "LacrimaFieldScene", "HavetFieldScene" };
    private List<string> bossStageNames = new List<string> { "FelixBossFieldScene", "PhobiaBossFieldScene", "OdiumBossFieldScene", "AmareBossFieldScene", "IrascorBossFieldScene", "LacrimaBossFieldScene", "HavetBossFieldScene" };
    private StageNode stageNode;
    private StageNode prevNode;
    private SpawnStage spawnStage;
    private GameObject managerOB;
    private DontDesManager manager;

    public void OnPointerClick(PointerEventData eventData)
    {
        managerOB = GameObject.Find("DontDesManager");
        manager = managerOB.GetComponent<DontDesManager>();
        stageNode = GetComponent<StageNode>();
        spawnStage = GetComponentInParent<SpawnStage>();
        sceneToLoad = stageNames[stageNode.type];
        sceneToLoadBoss = bossStageNames[stageNode.type];

        manager.SetStage(stageNode);

        if (stageNode.stat == "cleared" || stageNode.stat == "locked")
        {
            return;
        }

        for (int i = 0; i < stageNode.nextNodes.Count; i++)
        {
            stageNode.nextNodes[i].stat = "opened";
            stageNode.stat = "cleared";
            spawnStage.find_section(stageNode);
        }

        if (stageNode.isBoss)
        {
            VillageDataManager.Instance.ClearAllVillages();
            SceneManager.LoadScene(sceneToLoadBoss);
        }
        else
        {
            VillageDataManager.Instance.ClearAllVillages();
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void SetLineColor(StageNode node)
    {
        if (node.prevNodes.Count != 0)
        {
            prevNode = node.prevNodes[0];
            for (int i = 0; i < prevNode.lines.Count; i++)
            {
                if (prevNode.lines[i].endPoint == node.transform)
                {
                    continue;
                }
                else
                {
                    prevNode.lines[i].setInvalidColor();
                }

            }
        }
        for (int i = 0; i < node.lines.Count; i++)
        {
            stageNode.lines[i].setOpenColor();
        }
    }
}
