using UnityEngine;

namespace DitzelGames.FastIK
{
    public class FastIKLook : MonoBehaviour
    {
        /// <summary>
        /// Look at target
        /// </summary>
        public Transform Target;

        public enum FreezedAxisEnum {NONE,X,Y,Z};
        public FreezedAxisEnum FreezedAxis = FreezedAxisEnum.NONE;

        /// <summary>
        /// Initial direction
        /// </summary>
        protected Vector3 StartDirection;

        /// <summary>
        /// Initial Rotation
        /// </summary>
        protected Quaternion StartRotation;

        void Awake()
        {
            if (Target == null)
                return;

            StartDirection = Target.position - transform.position;
            StartRotation = transform.rotation;
        }

        void Update()
        {
            if (Target == null)
                return;
            Vector3 t_Pointer = Target.position;
            if (FreezedAxis != FreezedAxisEnum.NONE)
            {
                if(FreezedAxis == FreezedAxisEnum.X)
                {
                    transform.LookAt(new Vector3(transform.position.x, Target.position.y, Target.position.z));
                    return;
                } else if (FreezedAxis == FreezedAxisEnum.Y)
                {
                    transform.LookAt(new Vector3(Target.position.x, transform.position.y , Target.position.z));
                    return;
                } else if (FreezedAxis == FreezedAxisEnum.Z)
                {
                    transform.LookAt(new Vector3(Target.position.x, Target.position.y, transform.position.z));
                    return;
                }
            }
            
            transform.rotation = Quaternion.FromToRotation(StartDirection, Target.position - transform.position) * StartRotation;
            
            

        }
    }
}
