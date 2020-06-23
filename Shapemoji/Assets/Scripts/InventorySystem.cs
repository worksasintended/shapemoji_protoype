using UnityEngine;

// https://www.youtube.com/watch?v=w6_fetj9PIw
// https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
public class InventorySystem : MonoBehaviour
{
    [SerializeField] public GameObject[] ownedStones;
    [SerializeField] public GameObject[] inventorySlots;
    
    private void Start()
    {
        foreach (var slot in inventorySlots)
        {
            var position = slot.transform.position;
            var layer = slot.layer + 1;
            var index = UnityEngine.Random.Range(0, ownedStones.Length);
            GameObject stone = Instantiate(ownedStones[index], position, Quaternion.identity, slot.transform);
            stone.layer = layer;
        }
    }
}