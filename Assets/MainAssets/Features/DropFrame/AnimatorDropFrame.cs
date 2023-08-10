using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace UnityMiniFeatures.DropFrame
{
    /// <summary>
    /// Set animator simulation speed to update as target fps.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AnimatorDropFrame : MonoBehaviour
    {
        [SerializeField, Range(1, 120)] private int targetFps = 24;

        private Animator animator;
        private PlayableGraph graph;

        private float accumulatedDeltaTime;
        private bool updatedThisFrame;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            graph = animator.playableGraph;
            graph.Stop();
        }

        void Update()
        {
            var frameTime = 1f / targetFps;
            accumulatedDeltaTime += Time.deltaTime;
            if (accumulatedDeltaTime < frameTime) {
                return;
            }

            accumulatedDeltaTime -= frameTime;
            graph.Play();
            animator.speed = frameTime / Time.deltaTime;
            updatedThisFrame = true;
        }

        private void LateUpdate()
        {
            if (updatedThisFrame) {
                updatedThisFrame = false;
                graph.Stop();
            }
        }
    }
}
