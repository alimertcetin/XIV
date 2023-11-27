using UnityEngine;

namespace XIV.Core.Extensions
{
    public static class AnimatorExtensions
    {
        public static AnimationClip GetCurrentAnimation(this Animator animator)
        {
            return animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        }

        public static bool IsPlaying(this Animator animator, int stateNameHash)
        {
            return animator.GetCurrentAnimatorStateInfo(0).shortNameHash == stateNameHash;
        }
    }
}