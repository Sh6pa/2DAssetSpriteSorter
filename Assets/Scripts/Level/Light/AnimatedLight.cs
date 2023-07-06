using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class AnimatedLight: MonoBehaviour
{
    #region Serialized Filled
    [SerializeField]
    public Color m_spriteColor;
    [SerializeField]
    public bool m_isAnimated;
    [SerializeField] [Range(0.1f, 2f)]
    public float m_maxDelayOfRangeRefresh;
    [SerializeField] [Range(0f, 1.5f)]
    public float m_outerRadiusRange;
    [SerializeField] [Range(0f, 1f)]
    public float m_innerRadiusRange;
    #endregion

    #region protected
    protected Light2D _light2D;
    protected float _startOuterRadius;
    protected float _startInnerRadius;
    #endregion

    protected virtual void Start()
    {
        _light2D = GetComponent<Light2D>();
        _light2D.color = m_spriteColor;
        _startInnerRadius = _light2D.pointLightInnerRadius;
        _startOuterRadius = _light2D.pointLightOuterRadius;
        if (m_isAnimated)
        {
            AnimateLights();
        }
    }

    protected virtual void AnimateLights()
    {
        StopAllCoroutines();
        StartCoroutine(OuterRadiusHandler());
        StartCoroutine(InnerRadiusHandler());
    }

    protected virtual IEnumerator OuterRadiusHandler()
    {
        while (enabled)
        {
            float randomDuration = Random.Range(m_maxDelayOfRangeRefresh / 3, m_maxDelayOfRangeRefresh);

            float randomRange = Mathf.Abs(Random.Range(_startOuterRadius - m_outerRadiusRange, _startOuterRadius + m_outerRadiusRange));
            StartCoroutine(OuterRadiusAnimation(randomDuration, randomRange));
            yield return new WaitForSeconds(randomDuration);
        }
    }
    protected virtual IEnumerator InnerRadiusHandler()
    {
        while (enabled)
        {
            float randomDuration = Random.Range(m_maxDelayOfRangeRefresh / 3, m_maxDelayOfRangeRefresh);

            float randomRange = Mathf.Abs(Random.Range(_startInnerRadius - m_innerRadiusRange, _startInnerRadius + m_innerRadiusRange));
            StartCoroutine(InnerRadiusAnimation(randomDuration, randomRange));
            yield return new WaitForSeconds(randomDuration);
        }
    }

    protected virtual IEnumerator OuterRadiusAnimation(float duration, float finalRadius)
    {
        float timeCounter = 0;
        float outerNum = finalRadius - _light2D.pointLightOuterRadius;
        float stepForASecond = outerNum / duration;
        while (timeCounter <= duration)
        {
            timeCounter += Time.deltaTime;
            float nextOuterRadius = _light2D.pointLightOuterRadius + (Time.deltaTime * stepForASecond);
            if (nextOuterRadius > _light2D.pointLightInnerRadius)
            {
                _light2D.pointLightOuterRadius = nextOuterRadius;
            }
            yield return null;
        }
    }
    protected virtual IEnumerator InnerRadiusAnimation(float duration, float finalRadius)
    {
        float timeCounter = 0;
        float outerNum = finalRadius - _light2D.pointLightInnerRadius;
        float stepForASecond = outerNum / duration;
        while (timeCounter <= duration)
        {
            timeCounter += Time.deltaTime;
            float nextInnerRadius = _light2D.pointLightInnerRadius + (Time.deltaTime * stepForASecond);
            if (nextInnerRadius < _light2D.pointLightOuterRadius)
            {
                _light2D.pointLightInnerRadius = nextInnerRadius;
            }
            yield return null;
        }
    }
}
