#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace TaigaGames.Noises
{
    [CustomEditor(typeof(NoiseTexture2D))]
    public class NoiseTexture2DEditor : Editor
    {
        private Texture2D _texture;
        private SerializedProperty _noiseTexture;
        
        private void OnEnable()
        {
            _noiseTexture = serializedObject.FindProperty("_texture");
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
            if (!_texture)
            {
                EditorGUI.LabelField(r, "No noise texture selected.");
                return;
            }
            
            EditorGUI.DrawTextureTransparent(r, _texture);
        }

        public override bool HasPreviewGUI()
        {
            return true;
        }

        private void Redraw()
        {
            if (_noiseTexture.objectReferenceValue == null)
            {
                _texture = new Texture2D(1, 1);
                return;
            }
            
            _texture = _noiseTexture.objectReferenceValue as Texture2D;
        }
    }
}
#endif