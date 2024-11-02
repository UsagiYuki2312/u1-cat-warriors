using System;
using System.Collections.Generic;
using UnityEngine;


class Pool
{
    int nextId;
    // To make name

    Stack<GameObject> inactive;

    GameObject prefab;

    public Pool(GameObject prefabs, int initQuantify)
    {
        this.prefab = prefabs;

        //Intial stack
        inactive = new Stack<GameObject>(initQuantify);
    }

    // Method call sapwn
    public GameObject Spawn(Transform parent)
    {
        GameObject obj;

        if (inactive.Count == 0)
        {
            // Instatite if stack empty
            obj = GameObject.Instantiate(prefab, parent);

            if (nextId >= 10)
                obj.name = prefab.name + "_" + (nextId++);
            else
                obj.name = prefab.name + "_0" + (nextId++);

            obj.AddComponent<PoolIdentify>().pool = this;
        }
        else
        {
            //inactive.Shuffle();
            obj = inactive.Pop();
            if (obj == null)
                return Spawn(parent);
        }
        ISpawnable spawnable = obj.GetComponent<ISpawnable>();
        if (spawnable != null)
            spawnable.OnSpawn();
        obj.transform.SetParent(parent);
        obj.SetActive(true);

        return obj;
    }

    public void Despawn(GameObject obj)
    {
        if (!inactive.Contains(obj))
            inactive.Push(obj);
        obj.SetActive(false);
    }
}

class PoolIdentify : MonoBehaviour
{
    public Pool pool;
}

public class SmartPool : MonoBehaviour
{
    public GameObject Container
    {
        get
        {
            return this.gameObject;
        }
    }

    const int DEFAULT_POOL_SIZE = 5;

    Dictionary<GameObject, Pool> pools;
    // How infor pool

    // --INTIAL DICTIONARY FOR POOL--//
    void Init(GameObject prefabs = null, int quantify = DEFAULT_POOL_SIZE)
    {
        if (pools == null)
            pools = new Dictionary<GameObject, Pool>();

        if (prefabs != null && pools.ContainsKey(prefabs) == false)
            pools[prefabs] = new Pool(prefabs, quantify);
    }

    //--METHOD PRELOAD SOME OBJECT TO RESERVE--//
    public void Preload(GameObject prefab, int quantify)
    {
        Init(prefab, quantify);

        GameObject[] obs = new GameObject[quantify];
        for (int i = 0; i < quantify; i++)
            obs[i] = Spawn(prefab, null);


        for (int i = 0; i < quantify; i++)
            Despawn(obs[i]);
    }

    //--METHOD ACTIVE POOL OBJECT--//
    public GameObject Spawn(GameObject prefabs, Transform parent)
    {
        //Debug.Log (prefabs.name);
        Init(prefabs);
        return pools[prefabs].Spawn(parent);
    }

    //--METHOD DEACTIVE OBJECT POOL--
    public void Despawn(GameObject prefabs)
    {
        PoolIdentify poolIndent = prefabs.GetComponent<PoolIdentify>();

        if (poolIndent == null)
            prefabs.SetActive(false);
        else
        {
            prefabs.transform.SetParent(Container.transform);
            poolIndent.pool.Despawn(prefabs);
        }
    }

    public void PreloadResource(string pathFolder, int numberPreload)
    {
        GameObject[] fxPrefabs = Resources.LoadAll<GameObject>(pathFolder);
        for (int i = 0; i < fxPrefabs.Length; i++)
            Preload(fxPrefabs[i], numberPreload);
    }

    internal object Spawn(object pfFlyText, Vector3 zero, Quaternion identity)
    {
        throw new NotImplementedException();
    }
}
public interface ISpawnable
{
    void OnSpawn();
}
