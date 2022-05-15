using UnityEngine;
using TMPro;

namespace npcWorld
{
    public class Brush : MonoBehaviour
    {
        private Color[] _calculatedColors;
        private Texture2D _tesTex2D;
        private PlayerController _player;

        [SerializeField] private Camera _cam;
        [SerializeField] private Shader _drawShader;
        [SerializeField] private float _colorPercentage;
        [SerializeField] private Color _refColors;
        [SerializeField] private TextMeshProUGUI _percentText;

        private RenderTexture _splatMap;
        private Material _currentMat, _drawMat;
        private RaycastHit _hit;

        [SerializeField][Range(1, 500)] private float _size;
        [SerializeField][Range(0, 1)] private float _strength;

        private void Start()
        {
            _drawMat = new Material(_drawShader);
            _drawMat.SetVector("_Color", Color.red);

            _currentMat = GetComponent<MeshRenderer>().material;

            _splatMap = new RenderTexture(64, 64, 0, RenderTextureFormat.ARGBFloat);
            _currentMat.SetTexture("SplatMap", _splatMap);

            _tesTex2D = new Texture2D(64, 64, TextureFormat.ARGB32, false); //64 uzerine cikarsa performans problemleri oluyor default 64 ayarlandi

            _percentText.text = "Painted Surface : % 0.0";

            _player = PlayerController.InstancePlayer;

            if(_cam==null)
            {
                _cam = Camera.main;
            }
        }

        private void Update()
        {
            Debug.DrawRay(_cam.ScreenPointToRay(_player._inputHandler.MousePosition).origin, _cam.ScreenPointToRay(_player._inputHandler.MousePosition).direction*20,Color.green);

            if(_player._inputHandler.IsMousePreseed && _player.isFin && !_player.isEnd)
            {
                //if(Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition),out _hit)) //eski yontem
                if(Physics.Raycast(_cam.ScreenPointToRay(_player._inputHandler.MousePosition), out _hit))
                {
                    _drawMat.SetVector("_Coordinates", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
                    _drawMat.SetFloat("_Strength", _strength);
                    _drawMat.SetFloat("_Size", _size);
                    RenderTexture temp = RenderTexture.GetTemporary(_splatMap.width, _splatMap.height, 0, RenderTextureFormat.ARGBFloat);
                    Graphics.Blit(_splatMap, temp);
                    Graphics.Blit(temp, _splatMap, _drawMat);
                    RenderTexture.ReleaseTemporary(temp);

                    RenderTextureTo2DTexture(temp);

                    if(_colorPercentage == 100)
                    {
                        _player.EndScreen();
                        _player.isEnd = true;
                    }
                }
            }
        }

        private float CalculateSimilarity(Color[] colors, Color reference) //via https://gist.github.com/andrew-raphael-lukasik/73720c7a9aae0ff9faefd4f7b2a21660
        {
            Vector3 target = new Vector3 { x = reference.r, y = reference.g, z = reference.b };
            float accu = 0;
            const float sqrt_3 = 1.73205080757f;
            for (int i = 0; i < colors.Length; i++)
            {
                Vector3 next = new Vector3 { x = colors[i].r, y = colors[i].g, z = colors[i].b };
                accu += Vector3.Magnitude(target - next) / sqrt_3;
            }
            return 1f - ((float)accu / (float)colors.Length); //yuzde hesabi icin
        }

        private void RenderTextureTo2DTexture(RenderTexture rt)
        {
            _tesTex2D.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            _tesTex2D.Apply();
            _calculatedColors = _tesTex2D.GetPixels();
            _colorPercentage = Mathf.Round((CalculateSimilarity(_calculatedColors, _refColors) * 100f));

            if(_percentText !=null)
            {
                _percentText.text = "Painted Surface : %" + _colorPercentage;
            }
        }
    }
}