using UnityEngine;

public class CorridorManager : MonoBehaviour
{
    public static CorridorManager instance {  get; private set; }

    public Transform contentParent;
    public GameObject slotPrefab;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        var entries = Resources.LoadAll<CorridorEntry>("Minyoung/CorridorEntries");

        foreach (var entry in entries) 
        {
            GameObject slotObj = Instantiate(slotPrefab, contentParent);
            var slot = slotObj.GetComponent<CorridorSlot>();

            bool isCollected = LoadPlayerData(entry); // ���� ���� ���忡����(DB) ���濹��
            slot.Initialize(entry, isCollected);
        }
    }
    private bool LoadPlayerData(CorridorEntry entry)
    {
        //�ӽ� 
        return true;
    }
    public void OnSlotSelected(CorridorSlot slot)
    {
        if (!slot.isCollected)
        {
            Debug.Log("������������");
            return;
        }
        CorridorUIManager.instance.Show_DetailUI(slot.currentEntry);
    }

}
