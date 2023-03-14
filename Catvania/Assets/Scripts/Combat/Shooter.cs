using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Combat
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] GameObject projectilePrefab;
        [SerializeField] Transform firePoint;
        [SerializeField] Vector2 force;

        public void Shoot()
        {
            GameObject tempProjectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            tempProjectile.GetComponent<Rigidbody2D>().velocity = force * firePoint.lossyScale;
            tempProjectile.transform.localScale = firePoint.lossyScale;
        }
    }
}
