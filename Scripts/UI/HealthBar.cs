using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using UniRx;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    private SignalBus _signalBus;

    private bool _parentIsGameObject;
    private bool _parentIsCanvasOverlay;

    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _value;
    [SerializeField] private TextMeshProUGUI _change;

    private CompositeDisposable _disposable = new CompositeDisposable();

    [Inject]
    public void Construct(SignalBus signalBus) => _signalBus = signalBus;

    private void Awake()
    {
        _parentIsGameObject = GetComponentInParent<Unit>();
        _parentIsCanvasOverlay = transform.parent.GetComponent<Canvas>().renderMode == RenderMode.ScreenSpaceOverlay;
    }

    private void OnEnable()
    {
        //Debug.Log("OnEnable");
        _signalBus.GetStream<HealthChangedSignal>()
            .Where(x =>
            (_parentIsGameObject && x.Owner.GetType() == GetComponentInParent<IHasHealthHandler>().GetType())
            || (_parentIsCanvasOverlay && x.Owner.GetType() == typeof(Player)))
            .Subscribe(x =>
            {
                SetGui(x.HealthMax, x.Health);
                if (x.HealthLast > 0)
                    CreateChangeText(x.HealthChangedTo);
            }).AddTo(_disposable);
    }

    public void OnDisable()
    {
        //Debug.Log("OnDisable");
        _disposable.Clear();
    }

    private void SetGui(float healthMax, float health)
    {
        if (health < 0) return;

        _slider.DOValue(health / healthMax, 0.50f).SetEase(Ease.Linear);
        _value.text = Mathf.Ceil(health).ToString();
    }

    private void CreateChangeText(float healthChangedTo)
    {
        TextMeshProUGUI newText = Instantiate(_change, _change.transform.position, _change.transform.rotation);
        newText.gameObject.SetActive(true);
        newText.transform.SetParent(transform);
        newText.rectTransform.localScale = Vector3Int.one;
        newText.rectTransform.localPosition = new Vector3Int(
            Random.Range(-150, 150), 
            Random.Range(-50, 50), 
            0);

        Color newColor;
        string sing;

        if (healthChangedTo < 0)
        {
            newColor = new Color32(255, 70, 70, 255);
            sing = "-";
        }
        else
        {
            newColor = new Color32(0, 255, 120, 255);
            sing = "+";
        }

        newText.text = $"{sing} {Mathf.Abs(healthChangedTo)}";

        newText.DOColor(newColor, 0.1f).SetLink(newText.gameObject)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
                newText.DOColor(new Color32(255, 255, 255, 0), 0.6f).SetLink(newText.gameObject)
                .SetEase(Ease.Linear));

        newText.rectTransform.DOScale(new Vector3Int(3, 3, 3), 0.25f).SetLink(newText.gameObject)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
                newText.rectTransform.DOScale(new Vector3Int(1, 1, 1), 0.5f).SetLink(newText.gameObject)
                .SetDelay(0.1f)
                .SetEase(Ease.Linear)
                .OnComplete(() => Destroy(newText.gameObject)));
    }
}
