using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemySpawnInfo
{
    #region Editor Variables
    [SerializeField]
    private string m_Name;
    public string EnemyName
    {
        get
        {
            return m_Name;
        }
    }

    [SerializeField]
    private GameObject m_EnemyGo;
    public GameObject EnemyGo
    {
        get
        {
            return m_EnemyGo;
        }
    }

    [SerializeField]
    [Tooltip("The time we should wait before the first enemy is spawned.")]
    private float m_FirstSpawnTime;
    public float FirstSpawnTime
    {
        get { return m_FirstSpawnTime; }
    }

    [SerializeField]
    [Tooltip("How much faster the spawn's gonna be spawntime * increaserate .")]
    private float m_ReduceSpawnInterval;
    public float ReduceSpawnInterval
    {
        get { return m_ReduceSpawnInterval; }
    }
    #endregion
}
