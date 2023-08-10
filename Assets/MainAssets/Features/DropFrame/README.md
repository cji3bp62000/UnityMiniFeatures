→ [english below](#DropFrame_Component_for_Animator_/_ParticleSystem)

# AnimatorDropFrame / ParticleSystemDropFrame

Animator と ParticleSystem の更新フレームレートを落とし、アニメのような効果を得られるクラス。<br/>
また、更新していないときは完全停止のため、パフォーマンスにも優しい。

### 使い方

1. それぞれ対応のコンポーネントと同じ GameObject に DropFrame コンポーネントをアタッチする

### 実装説明

時間経過を計測して、特定の FPS の時間になったらコンポーネントを更新する。それ以外のフレームは一時停止する。

```C#
private float accumulatedTime;
private bool updatedThisFrame;

private void Update()
{
    accumulatedDeltaTime += Time.deltaTime;
    if (accumulatedDeltaTime < 1f / targetFps) {
        return;
    }

    // 再開して、1 フレーム分の時間を進む
    // (例) speed = frameTime / Time.deltaTime;
    updatedThisFrame = true;
}

private void LateUpdate()
{
    if (updatedThisFrame) {
        updatedThisFrame = false;
        // 再び一時停止する
    }
}
```

`Animator.Update()` や `ParticleSystem.Simulate()` といった、手動更新を使用しない理由は、元々の更新処理がマルチスレッドで、効率が高いからです。

※ `Animator` と `ParticleSystem` はどちらも `Update()` と `LateUpdate()` の間で更新処理が走っている。なので、`Update` 再開して、`LateUpdate` で再び一時停止できる。

---

# DropFrame Component for Animator / ParticleSystem

A class that lowers the update frame rate of Animator and ParticleSystem to obtain an animation-like effect.<br/>
Also, when not updated, it is completely stopped, so it is also friendly to performance.

### How to use

1. Attach the `(Animator/ParticleSystem)DropFrame` component to the same GameObject as the corresponding component.

### Implementation

Measure the elapsed time and update the component when the time of a specific FPS is reached. All other frames are paused (and not evaluated).

```C#
private float accumulatedTime;
private bool updatedThisFrame;

private void Update()
{
    accumulatedDeltaTime += Time.deltaTime;
    if (accumulatedDeltaTime < 1f / targetFps) {
        return;
    }

    // resume and advance time of one frame
    // (e.g.) speed = frameTime / Time.deltaTime;
    updatedThisFrame = true;
}

private void LateUpdate()
{
    if (updatedThisFrame) {
        updatedThisFrame = false;
        // Pause again
    }
}
```

The reason why manual update function, such as `Animator.Update()` and `ParticleSystem.Simulate()`, is not used is that the original update process is multi-threaded, and so more efficient.

※ Both `Animator` and `ParticleSystem` are running the update process between `Update()` and `LateUpdate()`. So, you can resume in `Update` and stop again in `LateUpdate`.