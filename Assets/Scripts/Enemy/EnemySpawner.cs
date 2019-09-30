using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemySpawner : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The different types of enemies that should be spawned and their corresponding spawn information.")]
    private EnemySpawnInfo[] m_EnemyTypes;
    #endregion

    #region Non-Editor Variables
    /* A timer for each enemy that should spawn an enemy of the corresponding
     * type when it reaches 0. Additionally, upon reaching 0, the value of the
     * timer should be reset to the appropriate value based on the enemy's spawn
     * rate. Therefore, we will have infinite spawning of the enemies.
     * 
     * Some challenges:
     * - Implement the spawning using a coroutine instead of this using this way
     * - Make the spawn rate ramp up (may require creating a mutator in the EnemySpawnInfo struct)
     */
    private float[] m_EnemySpawnTimers;

    // use to increase the spawn timer of the enemy
    private float[] m_DefaulSpawnTimer;
    #endregion

    #region First Time Initialization and Set Up
    private void Awake()
    {
        // Initialize the spawn timers using the FirstSpawnTime variable
        m_EnemySpawnTimers = new float[m_EnemyTypes.Length];
        m_DefaulSpawnTimer = new float[m_EnemyTypes.Length];
        for (int i = 0; i < m_EnemyTypes.Length; i++)
        {
            float spawnTime = m_EnemyTypes[i].FirstSpawnTime;
            m_EnemySpawnTimers[i] = spawnTime;
            m_DefaulSpawnTimer[i] = spawnTime;
        }
    }
    #endregion

    #region Main Updates
    private void Update()
    {
        // You may want to use either a foreach or for loop (for scalability)
        // Check if its time to spawn a particular enemy
        // If it is, just spawn the enemy using Instantiate(m_EnemyTypes[i].EnemyPrefab)
        // Make sure to reset the timer back to the appropriate value based on SpawnRate
        // Else, increase the timer using Time.deltaTime
        for (int i = 0; i < m_EnemySpawnTimers.Length; i++)
        {
            if (m_EnemySpawnTimers[i] > 0)
            {
                m_EnemySpawnTimers[i] -= Time.deltaTime;
            }
            else
            {
                Instantiate(m_EnemyTypes[i].EnemyGo);
                m_DefaulSpawnTimer[i] *= m_EnemyTypes[i].ReduceSpawnInterval;
                m_EnemySpawnTimers[i] = m_DefaulSpawnTimer[i];
            }
        }
    }
    #endregion
}