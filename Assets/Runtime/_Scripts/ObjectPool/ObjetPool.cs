using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjetPool<T> where T : MonoBehaviour, IPoolable
{
    [SerializeField] private Transform poolRoot;
    [SerializeField] private ObjetPoolParameters poolParameters;
    [SerializeField] private T prefab;
    
    private List<T> poolables;

    public void InitializePool()
    {
        poolables = new List<T>(poolParameters.initialSizePool);

        CreateObjects(poolParameters.initialSizePool);
    }

    private void CreateObjects(int quantityToCreate)
    {
         T CreateObject()
        {
            T instance = Object.Instantiate(prefab, poolRoot);
            instance.gameObject.SetActive(false);
            instance.OnInstantiated();
            poolables.Add(instance);
            return instance;
        }

        for (int i = 0; i < quantityToCreate; i++)
        {
            CreateObject();
        }
    }

    public T GetFromPoolOrCreate(Vector3 position, Quaternion quaternion, Transform parent)
    {
        T @object;

        T GetObject()
        {
            @object = poolables[poolables.Count - 1];
            poolables.RemoveAt(poolables.Count - 1);
            @object.gameObject.SetActive(true);
            @object.OnEnabledFromPool();
            return @object;
        }

        if (poolables.Count == 0)
        {
            CreateObjects(poolParameters.newResizePool);
        }

        @object = GetObject();
        SetupObject(@object, position, quaternion, parent);
        return @object;
    }

    public void ReturnToPool(T @object)
    {
        @object.OnDisabledFromPool();
        @object.gameObject.SetActive(false);
        @object.transform.SetParent(poolRoot);

        poolables.Add(@object);
    }

    private void SetupObject(T @object, Vector3 position, Quaternion quaternion, Transform parent)
    {
        @object.transform.SetPositionAndRotation(position, quaternion);
        @object.transform.SetParent(parent);
    }

}
