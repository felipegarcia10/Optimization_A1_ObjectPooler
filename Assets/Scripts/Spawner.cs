using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;
    
    [SerializeField] private float boundsX = 10f;
    [SerializeField] private float boundsY = 10f;

    private void Awake()
    {
        Instance = this;
    }

    public Vector3 GetRandomPositionInBounds()
    {
        return new Vector3(Random.Range(-boundsX * 0.5f, boundsX * 0.5f), 0, Random.Range(-boundsY * 0.5f, boundsY * 0.5f));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(boundsX, 0, boundsY));
    }
}
