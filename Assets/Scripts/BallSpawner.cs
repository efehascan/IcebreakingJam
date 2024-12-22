using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] Transform[] spawnPoints;
    
    Coroutine spawnCoroutine;
    
    readonly WaitForSeconds delay = new WaitForSeconds(1f);

    private void Start()
    {
        spawnCoroutine = StartCoroutine(Spawner());
    }


    void Spawn()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        var b = Instantiate(ball, spawnPoints[randomIndex].position, Quaternion.identity);
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
