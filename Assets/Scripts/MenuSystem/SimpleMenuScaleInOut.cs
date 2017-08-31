using UnityEngine;

public abstract class SimpleMenuScaleInOut<T> : Menu<T> where T : SimpleMenuScaleInOut<T>
{
    readonly Vector3 START_SCALE = Vector3.one * .25f;

    private bool animatingIn = false;
    private float currentAnimInTime = 0;
    private bool animatingOut = false;
    private float currentAnimOutTime = 0;

    // Prefab should have one child to scale
    private Transform visualRoot;

    protected override void Awake()
    {
        base.Awake();

        visualRoot = transform.GetChild(0);
        visualRoot.localScale = START_SCALE;
    }

    public static void Show()
    {
        Open();

        if (Instance.animInTime > 0)
        {
            Instance.animatingIn = true;
            Instance.currentAnimInTime = 0;
        }
    }

    public static void Hide()
    {
        Close();

        if (Instance.animOutTime > 0)
        {
            Instance.animatingOut = true;
            Instance.currentAnimOutTime = 0;
        }
    }

    public override void OnBackPressed()
    {
        Close();

        if (Instance.animOutTime > 0)
        {
            Instance.animatingOut = true;
            Instance.currentAnimOutTime = 0;
        }
    }

    private void Update()
    {
        if (animatingIn)
        {
            ScaleVisuals(ref Instance.animatingIn, ref currentAnimInTime, animInTime, START_SCALE, Vector3.one);
        }

        if (animatingOut)
        {
            ScaleVisuals(ref Instance.animatingOut, ref currentAnimOutTime, animOutTime, Vector3.one, START_SCALE);
        }
    }

    private void ScaleVisuals(ref bool isAnimating, ref float curAnimTime, float totalAnimTime, Vector3 startScale, Vector3 endScale)
    {
        curAnimTime += Time.deltaTime;
        if (curAnimTime >= totalAnimTime)
        {
            curAnimTime = totalAnimTime;
            isAnimating = false;
        }

        float percent = curAnimTime / totalAnimTime;

        visualRoot.localScale = Vector3.Lerp(startScale, endScale, percent);
    }
}
