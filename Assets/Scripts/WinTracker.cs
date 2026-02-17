using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTracker : MonoBehaviour
{
    // SAFE DATA: These are just numbers (Vector3). They don't care if the object dies.
    private Vector3[] zombieSpawnPositions;
    private Vector3 playerStartPos;

    // UNSAFE REFERENCES: These point to live objects. They become null when objects die.
    public ZombieController[] activeZombies;

    public ZombieController[] ZombiesToSpawnPrefabs;

    int currentDay = 1;
    NectarTex01 nectarGoal;
    PlayerController player;

    [SerializeField] GameObject ShopMenu;
    bool Shop = false;

    void Start()
    {
        ShopMenu.SetActive(false);
        player = FindAnyObjectByType<PlayerController>();
        nectarGoal = FindAnyObjectByType<NectarTex01>();

        // 1. Save the player's "Home" coordinates (Safe Data)
        playerStartPos = player.transform.position;

        // 2. Find the initial zombies
        activeZombies = FindObjectsByType<ZombieController>(FindObjectsSortMode.None);

        // 3. Save their "Home" coordinates into the SAFE array
        zombieSpawnPositions = new Vector3[activeZombies.Length];
        for (int i = 0; i < activeZombies.Length; i++)
        {
            // We copy the numbers (x,y,z) now, while the zombies are still alive
            zombieSpawnPositions[i] = activeZombies[i].transform.position;
        }
    }

    void Update()
    {
        if (Shop) return;

        if (player.nectarCollected >= nectarGoal.nectarTarget && nectarGoal.nectarTarget > 0)
        {
            GoToNextDay();
        }
    }

    public void GoToNextDay()
    {
        Shop = true;
        currentDay++;

        // DESTROY STEP: We wipe the actual objects from the scene.
        // After this loop runs, 'activeZombies' is full of null/broken references.
        foreach (ZombieController z in activeZombies)
        {
            if (z != null) Destroy(z.gameObject);
        }

        if (currentDay > 3)
        {
            SceneManager.LoadScene("Win");
        }
        else
        {
            ShopMenu.SetActive(true);
        }
    }

    public void LeaveShopButton()
    {
        ShopMenu.SetActive(false);

        // 1. Reset Player Logic
        player.nectarCollected = 0;
        nectarGoal.nectarTarget += 10;
        player.health = player.maxHealth;
        player.transform.position = playerStartPos; // Using the SAFE vector3

        // 2. Calculate the "Day Bonus"
        // Day 1 = 0 bonus. Day 2 = 50 bonus. Day 3 = 100 bonus.
        int healthBonus = (currentDay - 1) * 50;

        // 3. Spawn and Buff Zombies
        foreach (Vector3 spawnPos in zombieSpawnPositions)
        {
            int rand = Random.Range(0, ZombiesToSpawnPrefabs.Length);

            // CAPTURE the new zombie in a variable
            ZombieController newZombie = Instantiate(ZombiesToSpawnPrefabs[rand], spawnPos, Quaternion.identity);

            // APPLY the bonus immediately
            newZombie.health += healthBonus;
            
        }

        // 4. Re-populate the list so we can track/destroy them later
        activeZombies = FindObjectsByType<ZombieController>(FindObjectsSortMode.None);

        Shop = false;
    }

    public void BuyHealth()
    {
        if (player.nectarCollected >= 2)
        {
            player.nectarCollected -= 2;
            player.maxHealth += 10;
        }
    }

    public void BuyDamage()
    {
        if (player.nectarCollected >= 2)
        {
            player.nectarCollected -= 2;
            player.damage += 10;
        }
    }
}