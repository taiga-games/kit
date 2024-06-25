using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace TaigaGames
{
    [RequireComponent(typeof(Context))]
    public class ZenjectAutoSetup : MonoBehaviour
    {
        [SerializeField] private Context _context;

        private void Reset()
        {
            _context = GetComponent<Context>();
        }
        
        [Button("Set Installers")]
        private void SetInstallers()
        {
            SortAllTransformChildrenByName();
            var installers = GetComponentsInChildren<MonoInstaller>();
            _context.Installers = installers;
        }
        
        private void SortAllTransformChildrenByName()
        {
            var list = transform.Cast<Transform>().ToList();
            foreach (var child in list.OrderByDescending(x => x.name))
            {
                child.gameObject.name = child.gameObject.name.Replace("_Installer", "");
                child.SetSiblingIndex(0);
            }
        }
    }
}