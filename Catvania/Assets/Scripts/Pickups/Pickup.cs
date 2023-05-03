using Cat.Effects;
using UnityEngine;
using Cat.Flags;

namespace Cat.Pickups
{
    public abstract class Pickup : MonoBehaviour
    {
        [SerializeField] GameObject destroyEffect;
        [SerializeField] float flashHoldTime, flashDuration;

        FlaggedObject flaggedObject;

        private void Awake()
        {
            flaggedObject = GetComponent<FlaggedObject>();
        }

        private void Start()
        {
            if (flaggedObject.CheckFlag()) Destroy(gameObject);
        }

        protected abstract void OnPickUp(Collider2D other);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                OnPickUp(other);
                flaggedObject.SetFlag(true);
                other.GetComponent<SpriteFlasher>().Flash(flashHoldTime, flashDuration);
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
