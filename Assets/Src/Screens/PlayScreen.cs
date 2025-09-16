using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Test3
{
    public class PlayScreen : BaseScreen, IPointerDownHandler
    {
        [SerializeField] private Color cameraColor;
        [SerializeField] private Field fieldPrefab;
        [SerializeField] private Pendulum pendulumPrefab;
        [SerializeField] private float startForceFactor;
        [SerializeField] private float distance;

        private UpdateSource updateSource;
        private Timer timer;
        private Field field;
        private Pendulum pendulum;
        private Camera mainCamera;
        private ScreenService screenService;
        private MonoPool<CircleObject> circlesPool;
        private CircleObject last;
        private bool isAllowToTouch;
        
        public override void Init()
        {
            updateSource = ServiceLocator.Instance.Get<UpdateSource>();
            circlesPool = ServiceLocator.Instance.Get<MonoPool<CircleObject>>();
            screenService = ServiceLocator.Instance.Get<ScreenService>();
            mainCamera = ServiceLocator.Instance.Get<Camera>();
            
            pendulum = Instantiate(pendulumPrefab);
            pendulum.gameObject.SetActive(false);
            DontDestroyOnLoad(pendulum.gameObject);
            field = Instantiate(fieldPrefab);
            field.gameObject.SetActive(false);
            DontDestroyOnLoad(field.gameObject);
        }

        public override void Show()
        {
            pendulum.gameObject.SetActive(true);
            pendulum.Init(distance, Vector2.right * startForceFactor);

            mainCamera.backgroundColor = cameraColor;
            base.Show();
            field.Init();
            field.gameObject.SetActive(true);

            field.OnNext.AddListener(NextCircle);
            field.OnFinish.AddListener(() =>
            {
                timer?.Dispose();
            });
            
            NextCircle();
            
        }

        public override void Hide()
        {
            field.OnNext.RemoveAllListeners();
            field.OnFinish.RemoveAllListeners();
            pendulum.gameObject.SetActive(false);
            field.gameObject.SetActive(false);
            timer?.Dispose();
            base.Hide();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isAllowToTouch)
                return;
            
            isAllowToTouch = false;
            last = circlesPool.Spawn();
            last.gameObject.SetActive(true);
            pendulum.SetupOtherCircle(last);
            
            timer = new Timer(updateSource, () =>
            {
                circlesPool.Despawn(last);
                NextCircle();
            }, 4f);
        }

        private void NextCircle()
        {
            isAllowToTouch = true;
            timer?.Dispose();
            pendulum.SetColor((CircleColor)Random.Range(1, 3));
        }
    }
}