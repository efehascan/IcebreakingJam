using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private PlayerManagement player;
    [SerializeField] private float height;
    [SerializeField] private float Time = 3.0f;
    [SerializeField] Transform[] spawnPoints;
    
    Coroutine spawnCoroutine;
    
    readonly WaitForSeconds delay = new WaitForSeconds(0.5f);
    Vector3 spawnPoint = new Vector3(0, 4, 0);


    void Spawn()
    {
        var b = Instantiate(ball, spawnPoints[Random.Range(0, spawnPoints.Length)]);
    }
    
    IEnumerator Spawner()
    {
        while (true)
        {
            Spawn();
            yield return delay;
        }
    }

    #region On Disable and On Destroy

    private void OnDisable()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private void OnDestroy()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    #endregion
    
}
