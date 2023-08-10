→ [english below](#TimelineAnimationWeightSetter_en)

# TimelineAnimationWeightSetter

Timeline アニメーションの出力ウェイトを設定するクラス。<br/>
出力ウェイトを設定することで、元の Animator や他のTimeline を任意の比率でブレンドさせることができる。

### 使い方

1. `TimelineAnimationWeightSetter` コンポーネントを `PlayableDirector` と同じ GameObject にアタッチする
2. `TimelineAnimationWeightSetter` インスペクターでウェイトを設定して、結果を見る

### 実装説明
`AnimationPlayableOutput.SetWeight()` で Timeline のアニメーション出力ウェイトを設定することが出来る。
```C#
public void SetWeight(float weight)
{
    AnimationPlayableOutput output;
    output.SetWeight(weight);
}
```

ただし、Timeline はアニメーションの出力ウェイトを毎フレーム 1f に戻す処理がありますので、毎フレーム再設定する必要がある。<br/>
（通常の PlayableGraph の場合は一度だけで良い）

```C#
    public class SetAnimationOutputWeightBehaviour : PlayableBehaviour
    {
        public float weight;
        public List<AnimationPlayableOutput> outputList = new();

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            // weight を設定
            foreach (var output in outputList) {
                output.SetWeight(weight);
            }
        }
    }
```

---

# TimelineAnimationWeightSetter_en

A class that sets the output weight of the Timeline animation.<br/>
By setting the output weight, you can blend the Timeline animation with original Animator or other Timeline at any ratio.

### How to use

1. Attach the `TimelineAnimationWeightSetter` component to the same GameObject as `PlayableDirector`.
2. Set the weight variable in the `TimelineAnimationWeightSetter` inspector and see the result.

### Implementation

You can set the animation output weight of the Timeline with `AnimationPlayableOutput.SetWeight()`.

```C#
public void SetWeight(float weight)
{
    AnimationPlayableOutput output;
    output.SetWeight(weight);
}
```

However, Timeline has a process that resets the output weight of the animation to 1 every frame, so it is necessary to re-set the weight every frame.

```C#
    public class SetAnimationOutputWeightBehaviour : PlayableBehaviour
    {
        public float weight;
        public List<AnimationPlayableOutput> outputList = new();

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            // set weight
            foreach (var output in outputList) {
                output.SetWeight(weight);
            }
        }
    }
```