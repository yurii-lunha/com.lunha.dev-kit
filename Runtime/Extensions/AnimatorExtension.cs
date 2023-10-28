using System.Linq;
using UnityEngine;

namespace Lunha.DevKit.Extensions
{
    public static class AnimatorExtension
    {
        public static float GetAnimationClipLenght(this Animator animator, string clipName)
        {
            var clip = animator.runtimeAnimatorController.animationClips
                .FirstOrDefault(i => i.name.Equals(clipName));
            return clip ? clip.length : 0f;
        }
    }
}