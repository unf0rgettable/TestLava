using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "newPlayerSettings", menuName = "PlayerSettings", order = 0)]
    public class CharacterSettings : ScriptableObject
    {
        [SerializeField] private TypeBulletForce typeBulletForce;
        public TypeBulletForce TypeBulletForce => typeBulletForce;
        
        [SerializeField] private float speedCharacter;
        public float SpeedCharacter => speedCharacter;
        
        [SerializeField] private float speedAttack;
        public float SpeedAttack => speedAttack;
    }
}