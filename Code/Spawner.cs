using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] blocks;
    void Start()
    {
        SpawnBlock();
    }

    public void SpawnBlock()
    {
        Instantiate(blocks[Random.Range(0, blocks.Length)], transform.position, Quaternion.identity);
    }
}
