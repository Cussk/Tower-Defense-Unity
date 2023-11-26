using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damageToPlayer;
    public int moneyOnDeath;
    public float moveSpeed;

    Transform[] _path;
    int _currentPathWaypoint;

    void Start()
    {
        _path = GameManager.instance.enemyPath.waypoints;
    }

    void Update()
    {
        MoveAlongPath();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            GameManager.instance.AddMoney(moneyOnDeath);
            GameManager.instance.onEnemyDestroyed.Invoke();
            Destroy(gameObject);
        }
    }

    void MoveAlongPath()
    {
        if (_currentPathWaypoint < _path.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, _path[_currentPathWaypoint].position,
                moveSpeed * Time.deltaTime);

            if (transform.position == _path[_currentPathWaypoint].position)
                _currentPathWaypoint++;
        }
        else
        {
            GameManager.instance.TakeDamage(damageToPlayer);
            GameManager.instance.onEnemyDestroyed.Invoke();
            Destroy(gameObject);
        }
    }
}
