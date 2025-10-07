using System.Collections;
using TMPro;
using UnityEngine;

//This script is based on the Damage Popup tutorial from Christina Maraakis
//https://www.youtube.com/watch?v=2Jzl-fU8B0A

public class DamageLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private Color normalFontColor = Color.white;

    [SerializeField] private float startColorFadeAtPercent = 0.8f;
    [SerializeField] private AnimationCurve easeCurve;
    private float displayDuration;

    [SerializeField] private Vector2 highPointOffset = new Vector2(-350, 300);
    [SerializeField] private Vector2 lowPointOffset = new Vector2(-100, -500);
    [SerializeField] private float heightVariationMax = 150;
    [SerializeField] private float heightVariationMin = 50;

    private Vector3 highPointOffsetBasedOnDirection = Vector3.zero;
    private Vector3 dropPointOffsetBasedOnDirection = Vector3.zero;
    private bool direction = true;

    private SpawnsDamagePopups poolManager;
    private Coroutine moveCoroutine;

    private void OrientCurveBasedOnDirection()
    {
        highPointOffsetBasedOnDirection = highPointOffset;
        dropPointOffsetBasedOnDirection = lowPointOffset;

        if (direction)
            return;

        highPointOffsetBasedOnDirection.x = -highPointOffsetBasedOnDirection.x;
        dropPointOffsetBasedOnDirection.x = -dropPointOffsetBasedOnDirection.x;
    }

    private Vector3 CalcBezierPoint(float progress, Vector3 start, Vector3 control, Vector3 end)
    {
        float remainingPath = 1 - progress;
        Vector3 currentLocation = remainingPath * remainingPath * start;
        currentLocation += 2 * remainingPath * progress * control;
        currentLocation += progress * progress * end;

        return currentLocation;
    }

    public void Initialize(float displayDuration, SpawnsDamagePopups poolManager)
    {
        this.poolManager = poolManager;
        this.displayDuration = displayDuration;

        OrientCurveBasedOnDirection();
    }

    public void Display(float damage, Vector3 objPosition, bool direction)
    {
        transform.position = objPosition;
        this.direction = direction;

        damageText.SetText(damage.ToString());

        damageText.color = normalFontColor;
        //damageText.fontSize = isCrit ? critFontSize : normalFontSize;

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(Move());
        StartCoroutine(ReturnDamageLabelToPool(displayDuration));
    }

    private IEnumerator Move()
    {
        float time = 0;
        float fadeStartTime = startColorFadeAtPercent * displayDuration;

        OrientCurveBasedOnDirection();

        Vector3 start = transform.position;

        var heightVariation = Random.Range(heightVariationMin, heightVariationMax);
        Vector3 variation = new Vector3(0, heightVariation, 0);

        Vector3 highPoint = (start + highPointOffsetBasedOnDirection + variation);
        Vector3 dropPoint = highPoint + dropPointOffsetBasedOnDirection;

        while (time < displayDuration)
        {
            time += Time.deltaTime;

            float progress = time / displayDuration;
            float easedTime = easeCurve.Evaluate(progress);

            if (time > fadeStartTime)
            {
                Color color = damageText.color;
                float newAlpha = Mathf.Lerp(1, 0, (time - fadeStartTime) / (displayDuration - fadeStartTime));
                color.a = newAlpha;
                damageText.color = color;
            }

            transform.position = CalcBezierPoint(easedTime, start, highPoint, dropPoint);

            yield return null;
        }
    }

    private IEnumerator ReturnDamageLabelToPool(float displayLength)
    {
        yield return new WaitForSeconds(displayLength);
        poolManager.ReturnDamageLabelToPool(this);
    }
}
