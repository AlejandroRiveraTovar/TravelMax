using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs a spawnear")]
    public List<GameObject> prefabGameObjects;

    [Header("Opciones de spawn")]
    public float spawnInterval = 5f;   // cada cu�ntos segundos
    public Transform spawnPoint;       // punto de spawn, si no se asigna usa la posici�n del Spawner

    private float timer;

    void Start()
    {
        if (spawnPoint == null)
            spawnPoint = transform; // por defecto usa el transform del Spawner
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnRandomObject();
            timer = 0f; // reinicia el contador
        }
    }

    void SpawnRandomObject()
    {
        if (prefabGameObjects.Count == 0) return;

        int index = Random.Range(0, prefabGameObjects.Count);

        if (prefabGameObjects[index] != null)
        {
            Instantiate(prefabGameObjects[index], spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Prefab en �ndice " + index + " es NULL (probablemente fue destruido).");
        }
    }

}
