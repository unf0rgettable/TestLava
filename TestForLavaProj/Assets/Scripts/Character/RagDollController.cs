using UnityEngine;

namespace Character
{
    public class RagDollController : MonoBehaviour
    {
        void Start()
        {
            RagDollEnable(false);
        }

        public void RagDollEnable(bool enable)
        {
            foreach (var item in GetComponentsInChildren<Rigidbody>())
            {
                GetComponent<Animator>().enabled = !enable;
                item.isKinematic = !enable;
            }
        }
    }
}
