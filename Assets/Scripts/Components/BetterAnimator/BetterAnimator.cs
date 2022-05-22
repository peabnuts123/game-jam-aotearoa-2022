using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Components
{
    public class BetterAnimator : MonoBehaviour
    {
        // Config
        private const int DEFAULT_ANIMATION_LAYER = 0;

        // Public references
        [NotNull]
        [SerializeField]
        private Animator animator;

        // Private state
        private Dictionary<string, AnimationClip> animationClipManifest;
        // private string currentAnimationStateName;
        private string[] currentAnimationStates;
        // private bool currentAnimationRequiresInterruptOverride = false;
        private bool[] layerCurrentlyRequiresInterruptOverride;
        private IEnumerator[] layerCurrentAnimationCallback;

        void Start()
        {
            RebuildAnimationClipManifest();
            // currentAnimationStateName = GetCurrentAnimationClip(DEFAULT_ANIMATION_LAYER)?.name;
            currentAnimationStates = new string[animator.layerCount];
            for (int i = 0; i < animator.layerCount; i++)
            {
                currentAnimationStates[i] = GetCurrentAnimationClip(i)?.name;
            }
            layerCurrentlyRequiresInterruptOverride = new bool[animator.layerCount];
            layerCurrentAnimationCallback = new IEnumerator[animator.layerCount];
        }

        /// <summary>
        /// </summary>
        /// <param name="clipName">Animation clip name to play</param>
        /// <param name="interruptSelf">Whether the current animation should be interrupted even if it is the same as `clipName`</param>
        /// <param name="continueFromCurrentTime">Whether the new animation should not play from timestamp 0.0s, but rather, continue from the current time</param>
        /// <param name="interruptable">Whether the new animation can be interrupted without specifying `overrideUninterruptable = true`. Without it, other calls to `SetAnimation()` will be ignored until this animation clip is finished.</param>
        /// <param name="overrideUninterruptable">Required to interrupt an animation played with `interruptable = true`</param>
        public virtual void SetAnimation(string clipName,
            bool interruptSelf = false,
            bool continueFromCurrentTime = false,
            bool interruptable = true,
            bool overrideUninterruptable = false,
            int layer = DEFAULT_ANIMATION_LAYER
        )
        {
            if (interruptSelf)
            {
                // Explicitly specify start time, allows interrupting of self
                // Allows interrupting clips played with `interruptable = true` if it is the same as
                //  the clip being requested
                _PlayAnimation(clipName, interruptable, overrideUninterruptable || clipName == currentAnimationStates[layer], layer, normalizedStartTime: 0);
            }
            else if (continueFromCurrentTime)
            {
                // Play a different animation but continue from the current timestamp

                var stateInfo = animator.GetCurrentAnimatorStateInfo(layer);

                AnimationClip currentClip = GetCurrentAnimationClip(layer);
                float currentTimeOffset;
                if (currentClip != null)
                {
                    currentTimeOffset = stateInfo.normalizedTime * currentClip.length;
                }
                else
                {
                    // No clip currently playing so just default to 0
                    currentTimeOffset = 0;
                }
                AnimationClip newClip = GetAnimationClip(clipName);
                float newTimeOffsetNormalized = currentTimeOffset / newClip.length;
                _PlayAnimation(newClip.name, interruptable, overrideUninterruptable, layer, normalizedStartTime: newTimeOffsetNormalized);
            }
            else
            {
                // Just play an animation
                // If the animation is the same as the one playing, it will do nothing
                // if the animation is different than the one playing, it will start from the beginning
                _PlayAnimation(clipName, interruptable, overrideUninterruptable, layer);
            }
        }

        // @TODO rename `clipName` to `stateName`
        private void _PlayAnimation(string clipName, bool interruptable, bool overrideUninterruptable, int layer, float normalizedStartTime = float.NegativeInfinity)
        {
            // If the current animation requires override but none provided, do nothing
            if (layerCurrentlyRequiresInterruptOverride[layer] && !overrideUninterruptable)
            {
                return;
            }
            // otherwise either the current animation doesn't require the override or the override has been provided
            // which means either way we have the clearance needed to play the requested animation


            // Trigger the animation
            animator.Play(clipName, layer, normalizedStartTime);
            // currentAnimationStateName = clipName;
            currentAnimationStates[layer] = clipName;

            // If the animation is marked as "uninterruptable", kick off a timer to mark current clip as such until
            //  the clip ends. If the clip loops, it will become interruptable after its first play-through. The
            //  interruptable flag is not really intended for this scenario.
            if (!interruptable)
            {
                // Stop any existing "uninterruptable" timer (as the override has been provided)
                if (layerCurrentAnimationCallback[layer] != null)
                {
                    StopCoroutine(layerCurrentAnimationCallback[layer]);
                }

                // The clip we have just triggered (we need it to know how long to wait i.e. its length)
                AnimationClip newClip = animationClipManifest[clipName];

                // Mark as "uninterruptable" / requiring override
                layerCurrentlyRequiresInterruptOverride[layer] = true;

                // Kick off timer with callback for when timer completes
                layerCurrentAnimationCallback[layer] = WaitForAnimationFinishCoroutine(normalizedStartTime, newClip, () =>
                {
                    layerCurrentAnimationCallback[layer] = null;
                    layerCurrentlyRequiresInterruptOverride[layer] = false;
                });
                StartCoroutine(layerCurrentAnimationCallback[layer]);
            }
        }

        private IEnumerator WaitForAnimationFinishCoroutine(float normalizedStartTime, AnimationClip animationClip, Action onComplete)
        {
            yield return new WaitForSeconds(animationClip.length * (1F - Mathf.Max(normalizedStartTime, 0)));
            onComplete();
        }

        public void RebuildAnimationClipManifest()
        {
            animationClipManifest = new Dictionary<string, AnimationClip>();
            foreach (AnimationClip animationClip in animator.runtimeAnimatorController.animationClips)
            {
                animationClipManifest[animationClip.name] = animationClip;
            }
        }

        public AnimationClip GetCurrentAnimationClip(int layer)
        {
            var currentClips = animator.GetCurrentAnimatorClipInfo(layer);

            // Early return to prevent extra work
            if (currentClips.Length == 0)
            {
                return null;
            }
            else if (currentClips.Length == 1)
            {
                return currentClips[0].clip;
            }

            // Find the current clip with the largest weight
            // Taken to be "the current clip"
            // Presumably, this codepath will almost never happen
            // (most scenarios will just have 1 clip playing)
            float largestWeight = 0;
            int largestWeightClipIndex = -1;
            for (int i = 0; i < currentClips.Length; i++)
            {
                var clip = currentClips[i];
                if (clip.weight > largestWeight)
                {
                    largestWeight = clip.weight;
                    largestWeightClipIndex = i;
                }
            }

            return currentClips[largestWeightClipIndex].clip;
        }

        public AnimationClip GetAnimationClip(string clipName)
        {
            return animationClipManifest[clipName];
        }

        public Animator RawAnimator => animator;
    }
}