using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cam
{
    public class CameraEffectsController : MonoBehaviour
    {
        private Camera _camera;

        [SerializeField] private Material rippleMaterial;
        [SerializeField] private float maxAmount = 50f;

        [SerializeField] private float friction = .9f;

        [SerializeField] private float amount = 0f;
        private static readonly int _centerX = Shader.PropertyToID("_CenterX");
        private static readonly int _centerY = Shader.PropertyToID("_CenterY");
        private static readonly int _amountProperty = Shader.PropertyToID("_Amount");

        private Transform _transform;
        private readonly Vector3 _shakePosition = (Vector3.one - Vector3.back) * .25f;
        private Vector3 _defaultPosition;

        public Camera MainCamera => _camera;
        private bool _isShaking = false;
        
        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _transform = GetComponent<Transform>();
            _defaultPosition = _transform.position;
        }
        
        public void ShowRipple(Vector2 position)
        {
            _ = Ripple(position);
        }
        
        public void ShakeCamera()
        {
            if (_isShaking) return;

            _isShaking = true;
            DOTween.Sequence()
                .Append(_transform.DOShakePosition(1f, _shakePosition))
                .Append(_transform.DOMove(_defaultPosition, 0.25f))
                .AppendCallback(() => _isShaking = false);
        }
        
        private async UniTask Ripple(Vector2 pos)
        {
            amount = maxAmount;
            rippleMaterial.SetFloat(_centerX, pos.x);
            rippleMaterial.SetFloat(_centerY, pos.y);

            var startingTime = Time.time;
            while (Time.time - startingTime < 5)
            {
                rippleMaterial.SetFloat(_amountProperty, amount);
                amount *= friction;
                await UniTask.Yield();
            }

            amount = 0;
        }
        
        void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            Graphics.Blit(src, dst, rippleMaterial);
        }
    }
}