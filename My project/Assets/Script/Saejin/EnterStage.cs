using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

// UI 요소(Graphic) 에서 클릭을 받으려면 IPointerClickHandler
public class EnterStage : MonoBehaviour, IPointerClickHandler
{
    public string sceneToLoad;
    private List<string> stageNames = new List<string> { "FelixFieldScene", "PhobiaFieldScene", "OdiumFieldScene", "AmareFieldScene", "IrascorFieldScene", "LacrimaFieldScene", "HavetFieldScene" };
    private StageNode stageNode;

    public void OnPointerClick(PointerEventData eventData)
    {
        stageNode = GetComponent<StageNode>();
        sceneToLoad = stageNames[stageNode.type];
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogError("sceneToLoad 가 비어 있습니다!");
            return;
        }
        SceneManager.LoadScene(sceneToLoad);
    }
}
