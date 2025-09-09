using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Levels.Skins
{
    public class SkinElement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image m_image;

        public void SetImageSprite(Sprite sprite)
        {
            m_image.sprite = sprite;
        }
    }
}