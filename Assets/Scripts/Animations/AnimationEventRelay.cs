using UnityEngine;

namespace Animations {
    public class AnimationEventRelay : MonoBehaviour {
        private EntityStats rootStats;

        private void Start() {
            rootStats = GetComponentInParent<EntityStats>();
        }

        public void TriggerAttackHit() {
            if (rootStats != null) {
                rootStats.ExecuteAttackHit();
            }
        }
    }
}