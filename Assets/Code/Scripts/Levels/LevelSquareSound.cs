using UnityEngine;

namespace KronosTech.Levels
{
    public class LevelSquareSound : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AudioSource _source;

        private const float k_basePitch = 0.5f;
        private const float k_multiplierPitch = 0.25f;

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

        private void PlaySound(LevelSquareInteractEventArgs args)
        {
            if (args.ForcedInteract)
            {
                return;
            }

            _source.pitch = k_basePitch + k_multiplierPitch * args.Value;

            _source.Play();
        }
    }
}