using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace UnityMiniFeatures.TimelineAnimationWeightSetter
{
    /// <summary>
    /// Timeline のアニメーションのウェイトを設定する PlayableBehaviour クラス。<br/>
    /// This behaviour is used to set the weight of the animation output.
    /// </summary>
    public class SetAnimationOutputWeightBehaviour : PlayableBehaviour
    {
        public float weight;
        public List<AnimationPlayableOutput> outputList = new();

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            // weight を設定
            // set weight
            foreach (var output in outputList) {
                output.SetWeight(weight);
            }
        }
    }

    /// <summary>
    /// SetAnimationOutputWeightBehaviour をサポートするクラス。PlayableDirector と同じ GameObject にアタッチしてください。<br/>
    /// This component is used create and support the SetAnimationOutputWeightBehaviour.<br/>
    /// Attach this to the same GameObject as the PlayableDirector.
    /// </summary>
    public class TimelineAnimationWeightSetter : MonoBehaviour
    {
        /// <summary> weight of animation outputs (set every frame) </summary>
        [SerializeField, Range(0f, 1f)] private float weight = 0.5f;

        private SetAnimationOutputWeightBehaviour behaviour;

        public void SetWeight(float newWeight)
        {
            weight = newWeight;
        }

        private void Awake()
        {
            var director = GetComponent<PlayableDirector>();
            if (!director) return;

            // イベントを登録
            // register events
            director.played += CreateSetWeightBehaviour;
            director.stopped += OnPlayableDirectorStopped;

            // すでに再生中の場合は、Behaviour を手動で作成します。(playOnAwake が Playing をトリガーした場合、director.played イベントは呼び出されません)
            // If the director is already playing, manually create the behaviour. (playOnAwake triggered Playing wont call director.played event)
            if (director.playOnAwake && director.playableGraph.IsValid()) {
                CreateSetWeightBehaviour(director);
            }
        }

        private void CreateSetWeightBehaviour(PlayableDirector director)
        {
            var graph = director.playableGraph;
            if (!graph.IsValid()) return;

            // Behaviour を追加
            // add behaviour
            var playable = ScriptPlayable<SetAnimationOutputWeightBehaviour>.Create(graph);
            var output = ScriptPlayableOutput.Create(graph, "SetAnimationOutputWeight");
            output.SetSourcePlayable(playable);
            behaviour = playable.GetBehaviour();

            // initialize behaviour
            // Behaviour を初期化
            behaviour.weight = weight;
            var outputCount = graph.GetOutputCountByType<AnimationPlayableOutput>();

            // Behaviour にすべての Output を追加
            // (ここを拡張して、制御したい Output のみを追加することもできます)
            // add all outputs to behaviour
            // you can extend this to only add the outputs you want to control
            for (var i = 0; i < outputCount; i++) {
                behaviour.outputList.Add((AnimationPlayableOutput)graph.GetOutputByType<AnimationPlayableOutput>(i));
            }
        }

        private void OnPlayableDirectorStopped(PlayableDirector obj)
        {
            behaviour = null;
        }

        private void Update()
        {
            if (behaviour == null) return;

            // Behaviour の weight を更新
            // update behaviour
            behaviour.weight = weight;
        }
    }
}
