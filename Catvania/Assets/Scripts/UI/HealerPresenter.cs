using Cat.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.UI
{
    public class HealerPresenter : MonoBehaviour
    {
        [SerializeField] Healer healer;
        [SerializeField] FlaskUI flaskUIPrefab;

        private void OnEnable()
        {
            healer.healEvent += UpdateHeals;
        }

        private void Start()
        {
            UpdateHeals();
        }

        void UpdateHeals()
        {
            ClearLayoutGroup();

            for(int i = 0; i < healer.GetHealCount(); i++)
            {
                FlaskUI flaskUI = Instantiate(flaskUIPrefab, transform);
                flaskUI.Setup(i < healer.GetHealsRemaining());
            }
        }

        private void ClearLayoutGroup()
        {
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void OnDisable()
        {
            healer.healEvent -= UpdateHeals;
        }
    }
}
