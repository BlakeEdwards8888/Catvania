using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cat.UI
{
    public class FlaskUI : MonoBehaviour
    {
        [SerializeField] Sprite filledSprite, emptySprite;

        public void Setup(bool isFilled)
        {
            GetComponent<Image>().sprite = isFilled ? filledSprite : emptySprite;
        }
    }
}
