using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum TowerTargetPriority
    {
        First,
        Close,
        Strong
    }

    [Header("Info")] 
    public TowerTargetPriority towerTargetPriority;
    public float range;
    public bool rotateTowardsTarget;

    List<Enemy> _currentEnemiesInRange = new List<Enemy>();
    Enemy _currentEnemy;

    [Header("Attacking")] 
    public GameObject projectilePrefab;
    public Transform projectileSpawnPosition;
    public float attackRate;
    public float projectileSpeed;
    public int projectileDamage;

    float _lastAttackTime;

    private void Update()
    {
        CheckTimeSinceLastAttack();
    }

    void CheckTimeSinceLastAttack()
    {
        if (!(Time.time - _lastAttackTime > attackRate)) return;
        _lastAttackTime = Time.time;
        _currentEnemy = GetTargetEnemy();

        if (_currentEnemy != null)
            Attack();
    }

    Enemy GetTargetEnemy()
    {
        _currentEnemiesInRange.RemoveAll(x => x == null);

        if (_currentEnemiesInRange.Count == 0) return null;
        if (_currentEnemiesInRange.Count == 1) return _currentEnemiesInRange[0];

        switch (towerTargetPriority)
        {
            case TowerTargetPriority.First:
                return _currentEnemiesInRange[0];
            case TowerTargetPriority.Close:
                return GetClosestEnemy();
            case TowerTargetPriority.Strong:
                return GetStrongestEnemy();
        }
        
        return null;
    }

    Enemy GetStrongestEnemy()
    {
        Enemy strongestEnemy = null;
        var strongestHealth = 0;

        foreach (var enemy in _currentEnemiesInRange)
        {
            if (enemy.health > strongestHealth)
            {
                strongestEnemy = enemy;
                strongestHealth = enemy.health;
            }
        }

        return strongestEnemy;
    }

    Enemy GetClosestEnemy()
    {
        Enemy closestEnemy = null;
        var maxDistance = 99f;

        foreach (var enemy in _currentEnemiesInRange)
        {
            var distanceToEnemy = (transform.position - enemy.transform.position).sqrMagnitude;

            if (maxDistance < distanceToEnemy)
            {
                closestEnemy = enemy;
                distanceToEnemy = maxDistance;
            }
        }

        return closestEnemy;
    }

    void Attack()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _currentEnemiesInRange.Add(other.GetComponent<Enemy>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _currentEnemiesInRange.Remove(other.GetComponent<Enemy>());
        }
    }
}
