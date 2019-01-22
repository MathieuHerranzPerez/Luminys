using UnityEngine;
using UnityEngine.UI;

public class BlockWaveControler : MonoBehaviour
{
    [SerializeField]
    private Text chronoText;
    private float chrono = 0f;

    [SerializeField]
    private float chronoBetweenSpawn = 1f;
    [SerializeField]
    private float changeSpawnRateTime = 25f;
    private float currentChangeSPawnRate = 1f;
    private float changeSpawnRateChrono = 0f;
    private float chronoSpawn = 5f;

    private BlockSpawner[] arraySpawner;
    private int nbSpawnPoint;

    private int previousIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        // get all the spawners
        nbSpawnPoint = transform.childCount;
        arraySpawner = new BlockSpawner[nbSpawnPoint];
        for (int i = 0; i < nbSpawnPoint; ++i)
            arraySpawner[i] = transform.GetChild(i).GetComponent<BlockSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        chrono += Time.deltaTime;
        chrono = Mathf.Clamp(chrono, 0f, Mathf.Infinity);
        chronoText.text = string.Format("{0:00.0}", chrono);

        chronoSpawn -= Time.deltaTime;
        if(chronoSpawn <= 0f)
        {
            Spawn();
            chronoSpawn = chronoBetweenSpawn;
        }

        changeSpawnRateChrono += Time.deltaTime;
        if(changeSpawnRateChrono >= currentChangeSPawnRate)
        {
            if (chronoBetweenSpawn > 0.1f)
            {
                chronoBetweenSpawn -= 0.1f;
                currentChangeSPawnRate = changeSpawnRateTime - (changeSpawnRateTime * chronoBetweenSpawn);
            }
            else
                chronoBetweenSpawn = 0.1f;

            changeSpawnRateChrono = 0f;
        }
    }

    private void Spawn()
    {
        int index = Random.Range(0, nbSpawnPoint);
        if (index != previousIndex)
        {
            previousIndex = index;
            arraySpawner[index].SpawnBlock();
        }

    }

    public float GetChrono()
    {
        return chrono;
    }
}
