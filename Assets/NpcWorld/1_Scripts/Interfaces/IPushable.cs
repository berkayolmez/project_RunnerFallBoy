using UnityEngine;

namespace npcWorld
{
    public interface IPushable
    {
        void PushPlayer(Vector3 pushDirection,float pushForce);

        void AddImpact(Vector3 impactDirection, float impacthForce);
    }
}