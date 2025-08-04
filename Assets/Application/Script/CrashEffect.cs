using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class CrashEffect : MonoBehaviour
{
    public Volume volume;

    private ChromaticAberration chromatic;
    private LensDistortion lensDistortion;

    private void Start()
    {
        volume.profile.TryGet(out chromatic);
        volume.profile.TryGet(out lensDistortion);
    }

    [DrawButton]
    public void TriggerCrashEffect()
    {
        StartCoroutine(CrashRoutine());
    }

    private IEnumerator CrashRoutine()
    {
        float duration = 0.05f;
        float restoreDuration = 0.05f;


        // Animate the crash effect
        float time = 0f;
        while (time < duration)
        {
            float t = time / duration;
            if (chromatic != null) chromatic.intensity.value = Mathf.Lerp(0f, 1f, t);
            if (lensDistortion != null) lensDistortion.intensity.value = Mathf.Lerp(0f, -0.6f, t);
            time += Time.deltaTime;
            yield return null;
        }


        // yield return new WaitForSeconds(0.2f);

        // Restore the effect back to normal
        time = 0f;
        while (time < restoreDuration)
        {
            float t = time / restoreDuration;
            if (chromatic != null) chromatic.intensity.value = Mathf.Lerp(1f, 0f, t);
            if (lensDistortion != null) lensDistortion.intensity.value = Mathf.Lerp(-0.6f, 0f, t);
            time += Time.deltaTime;
            yield return null;
        }

        // Final reset to ensure exact zero
        if (chromatic != null) chromatic.intensity.value = 0f;
        if (lensDistortion != null) lensDistortion.intensity.value = 0f;
    }
}
