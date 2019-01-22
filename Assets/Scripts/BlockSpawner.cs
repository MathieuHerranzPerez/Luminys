using System.Collections;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject block;
    public float initialVelocity;

    private GameObject light;
    

    // Start is called before the first frame update
    void Start()
    {
        light = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBlock()
    {
        StartCoroutine(Light());
        
    }

    private IEnumerator Light()
    {
        float t = 0f;
        light.SetActive(true);
        while (t < 0.50f)
        {
            t += Time.deltaTime;
            yield return 0;
        }
        light.SetActive(false);
        GameObject spawnedBlock = Instantiate(block, transform.position, Quaternion.identity);
        spawnedBlock.GetComponent<Rigidbody2D>().velocity = transform.right * initialVelocity;
    }
}

