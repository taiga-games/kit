#if UNITY_EDITOR
using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace TaigaGames.Noises
{
    [CustomEditor(typeof(NoiseGenerationSettings))]
    public class NoiseGenerationSettingsEditor : Editor
    {
        private static readonly int2 _outputTextureSize = new int2(256, 256);
        private Texture2D _outputTexture;

        private SerializedProperty _seed;
        
        private NoiseGenerationSettings Target => (NoiseGenerationSettings) target;

        private void OnEnable()
        {
            _outputTexture ??= new Texture2D(_outputTextureSize.x, _outputTextureSize.y);
            _seed = serializedObject.FindProperty("_seed");
            Redraw();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();
            if (EditorGUI.EndChangeCheck())
                Redraw();
        }

        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            EditorGUI.DrawTextureTransparent(r, _outputTexture);
        }

        public override bool HasPreviewGUI()
        {
            return true;
        }

        private void Redraw()
        {
            var bakedNoise = Target.BakeFlatNoise(_seed.intValue, _outputTextureSize, float2.zero);

            try
            {
                for (var x = 0; x < _outputTextureSize.x; x++)
                for (var y = 0; y < _outputTextureSize.y; y++)
                {
                    var noise = bakedNoise.GetNoise(x, y);
                    _outputTexture.SetPixel(x, y, new Color(noise, noise, noise, 1f));
                }

                _outputTexture.Apply();
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to draw noise texture: " + e.Message + "\n" + e.StackTrace);
            }
            finally
            {
                bakedNoise.Dispose();
            }
        }
    }
}
#endif