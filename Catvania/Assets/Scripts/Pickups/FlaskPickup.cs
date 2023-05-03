using Cat.Combat;
using Cat.Controls;
using Cat.Flags;
using UnityEngine;

namespace Cat.Pickups
{
    public class FlaskPickup : InteractableObject
    {
        readonly int InactiveHash = Animator.StringToHash("Inactive");
        readonly int EmptyHash = Animator.StringToHash("Empty");

        Animator animator;
        FlaggedObject flaggedObject;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
            flaggedObject = GetComponent<FlaggedObject>();
        }

        private void Start()
        {
            if (flaggedObject.CheckFlag())
            {
                animator.Play(InactiveHash);
            }
        }

        protected override void ExecuteAction()
        {
            playerTransform.GetComponent<Healer>().AddHeal();
            animator.Play(EmptyHash);
            flaggedObject.SetFlag(true);
        }

        protected override bool IsInteractable()
        {
            return !flaggedObject.CheckFlag() && base.IsInteractable();
        }
    }
}
