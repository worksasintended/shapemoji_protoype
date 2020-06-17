using UnityEngine;

// https://www.youtube.com/watch?v=w6_fetj9PIw
// https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
public class InventorySystem : MonoBehaviour
{
    [SerializeField] public GameObject[] ownedStones;
    [SerializeField] public GameObject[] inventarSlots;
    [SerializeField] public GameObject inventar;
    
    private void Start()
    {
        foreach (var slot in inventarSlots)
        {
            // GameObject parent = Instantiate(slot, slot.transform.position, Quaternion.identity, inventar.transform);
            var position = slot.transform.position;
            var layer = slot.layer + 1;
            var index = UnityEngine.Random.Range(0, ownedStones.Length);
            // GameObject stone = Instantiate(ownedStones[index], parent.transform.position, Quaternion.identity, parent.transform);
            GameObject stone = Instantiate(ownedStones[index], position, Quaternion.identity, slot.transform);
            // GameObject stone = Instantiate(ownedStones[index], position, Quaternion.identity);
            // stone.transform.parent = slot.transform;
            // GameObject stone = PrefabUtility.InstantiatePrefab(ownedStones[index]) as GameObject;
            stone.layer = layer;
        }
    }
}