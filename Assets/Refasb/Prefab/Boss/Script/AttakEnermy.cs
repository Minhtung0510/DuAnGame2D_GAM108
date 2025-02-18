using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakEnermy : MonoBehaviour
{
        public float speed;
        private Transform player;
        private Vector2 target;
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Hitbox").transform;
            target = new Vector2(player.position.x, player.position.y);
        }
        void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position.x == target.x && transform.position.y == target.y)
            {
                DestroyProjectile();
            }
        }
        private void DestroyProjectile()
        {
            Destroy(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                DestroyProjectile();
            }
        }
    }


