using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Enemy _targetEnemy;
    int _damage;
    float _moveSpeed;

    public GameObject hitSpawnPrefab;

    public void Initialize(Enemy targetEnemy, int damage, float moveSpeed)
    {
        _targetEnemy = targetEnemy;
        _damage = damage;
        _moveSpeed = moveSpeed;
    }

    void Update()
    {
        MoveTowardsTargetEnemy();
    }

    void MoveTowardsTargetEnemy()
    {
        if (_targetEnemy != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetEnemy.transform.position, _moveSpeed * Time.deltaTime);

            transform.LookAt(_targetEnemy.transform);

            SpawnProjectile();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void SpawnProjectile()
    {
        if (Vector3.Distance(transform.position, _targetEnemy.transform.position) < 0.2f)
        {
            _targetEnemy.TakeDamage(_damage);

            if (hitSpawnPrefab != null)
                Instantiate(hitSpawnPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
