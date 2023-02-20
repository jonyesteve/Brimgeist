using UnityEngine;
namespace Utilities
{
    public static class General
    {
        public static bool IsAnimationPlaying(Animator ani, string stateName)
        {
            if (ani.GetCurrentAnimatorStateInfo(0).IsName(stateName)
                && ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0)
            {
                return true;
            }
            else return false;
        }
        public static bool IsAnimationPlaying(Animator ani, string stateName, float time)
        {
            if (ani.GetCurrentAnimatorStateInfo(0).IsName(stateName)
                && ani.GetCurrentAnimatorStateInfo(0).normalizedTime > time)
            {
                return true;
            }
            else return false;
        }
    }
}