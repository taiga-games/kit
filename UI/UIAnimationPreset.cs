using UnityEngine;

namespace TaigaGames.Kit
{
    [CreateAssetMenu(menuName = "TaigaGames/UI/UI Animation Function Preset", fileName = "UI Animation Function Preset")]
    public class UIAnimationPreset : ScriptableObject
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private EasingType _easingType = EasingType.InOutSine;
        
        public float Speed => _speed;
        public EasingType EasingType => _easingType;
    }
}