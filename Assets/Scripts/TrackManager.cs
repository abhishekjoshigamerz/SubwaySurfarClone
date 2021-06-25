using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    [SerializeField] GameObject[] tilePrefabs;
    [SerializeField] float SpawnPositionZ = 0;
    [SerializeField] float length = 1;
    [SerializeField] int NumberofTilesVisible = 5;
    [SerializeField]  Transform playerPositionChecker;
    private List<GameObject> activeTiles = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i =0;i < NumberofTilesVisible;i++){
            if(i==0){
                SpawnTile(0);
            }
            SpawnTile(Random.Range(1,tilePrefabs.Length-1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerPositionChecker.position.z - 35 > SpawnPositionZ - (NumberofTilesVisible * length)){
            SpawnTile(Random.Range(1,tilePrefabs.Length-1));
            DeleteTile();
        }
    }
    void SpawnTile(int TileIndex){
        GameObject go =  Instantiate(tilePrefabs[TileIndex],transform.forward * SpawnPositionZ, transform.rotation);
        SpawnPositionZ+= length;
        activeTiles.Add(go);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
