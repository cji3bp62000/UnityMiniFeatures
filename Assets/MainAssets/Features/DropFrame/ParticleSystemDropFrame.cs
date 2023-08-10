using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Timeline;

namespace UnityMiniFeatures.DropFrame
{
    /// <summary>
    /// Set particle system simulation speed to update as target fps.
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleSystemDropFrame : MonoBehaviour
    {
        [SerializeField, Range(1, 120)] private int targetFps = 24;

        private ParticleSystem rootPs;
        private ParticleSystem.MainModule[] mains;

        private float accumulatedDeltaTime;
        private bool updatedThisFrame;

        void Start()
        {
            rootPs = GetComponent<ParticleSystem>();

            using var _ = ListPool<ParticleSystem>.Get(out var tempPsList);
            GetComponentsInChildren<ParticleSystem>(tempPsList);
            mains = tempPsList.Select(ps => ps.main).ToArray();

            rootPs.Pause(true);
            SetAllSimulationSpeed(0f);
        }

        void Update()
        {
            var frameTime = 1f / targetFps;
            accumulatedDeltaTime += Time.deltaTime;
            if (accumulatedDeltaTime < frameTime) {
                return;
            }

            accumulatedDeltaTime -= frameTime;
            rootPs.Play(true);
            SetAllSimulationSpeed(frameTime / Time.deltaTime);
            updatedThisFrame = true;
        }

        void LateUpdate()
        {
            if (updatedThisFrame) {
                updatedThisFrame = false;
                rootPs.Pause(true);
            }
        }

        private void SetAllSimulationSpeed(float speed)
        {
            for (var i = 0; i < mains.Length; i++) {
                mains[i].simulationSpeed = speed;
            }
        }
    }
}
