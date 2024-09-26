using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    void Awake(){
        if(instance == null)
            instance = this;
        else
            Destroy(this);
        
        PoolLineRenderers();
    }

    [SerializeField]
    GameObject lineRendererPrefab;
    private List<GameObject> lineRendererPool = new List<GameObject>();
    int numToPool = 100;
    void PoolLineRenderers(){
        for(int i = 0; i < numToPool; i++){
            lineRendererPool.Add(Instantiate(lineRendererPrefab, Vector3.down, Quaternion.identity, transform));
            lineRendererPool[i].SetActive(false);
        }
    }
    public GameObject GiveLineRenderer(){
        foreach(GameObject g in lineRendererPool)
            if(!g.activeInHierarchy){
                g.SetActive(true);
                return g;
            }
        return null;
    }

    public void ReturnAllToPool(){
        foreach(GameObject g in lineRendererPool)
            g.SetActive(false);
    }
}
