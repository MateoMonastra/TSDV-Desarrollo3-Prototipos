using UnityEngine;

namespace Challenge
{
    public abstract class Challenge : MonoBehaviour
    {
        protected abstract void Init();
        protected abstract void Tick();
        protected abstract void End();
    }
}