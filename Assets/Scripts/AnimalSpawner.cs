using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public Rbit rabbitPrefab;
    List<Rbit> rabbits = new List<Rbit>();
    public BoxCollider2D boxCollider;

    [Range(1f, 300f)]
    public int startingCount = 50;
    private float maxX, maxY, minX, minY;


    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        maxX = boxCollider.transform.position.x + (boxCollider.size.x / 2);
        minX = boxCollider.transform.position.x - (boxCollider.size.x / 2);
        maxY = boxCollider.transform.position.y + (boxCollider.size.y / 2);
        minY = boxCollider.transform.position.y - (boxCollider.size.y / 2);

        for (int i = 0; i < startingCount; i++)
        {
            float spawnX = Random.Range(minX, maxX);
            float spawnY = Random.Range(minY, maxY);
            Rbit newRbit = Instantiate(rabbitPrefab, new Vector2(spawnX, spawnY), Quaternion.Euler(Vector3.forward), transform);
            newRbit.name = "Rbit " + i;
            rabbits.Add(newRbit);
        }
    }
}
