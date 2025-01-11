using UnityEngine;

namespace KronosTech.Levels
{
    public class LevelSquareSound : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AudioSource _source;

        private LevelSquare _square;

        private void OnEnable()
        {
            _square.OnInteract += PlaySound;
        }
        private void OnDisable()
        {
            _square.OnInteract -= PlaySound;
        }
        private void Awake()
        {
            _square = GetComponent<LevelSquare>();
        }

        private void PlaySound(sbyte value, bool forced)
        {
            if (forced)
            {
                return;
            }

            _source.pitch = value == 0 ? 0.5f : (0.75f + 0.25f * value);
            _source.Play();
        }
    }
}