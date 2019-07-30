using UnityEditor;
using UnityEngine;

namespace Plugins.Editor
{
    public class ColliderToFit : MonoBehaviour {
     
        [MenuItem("Stencil/Fit Collider")]
        private static void FitToChildren() {
            foreach (var rootGameObject in Selection.gameObjects) {
                var collider = rootGameObject.GetComponent<BoxCollider>();
                if (collider == null) return;

                var scale = rootGameObject.transform.localScale;
                rootGameObject.transform.localScale = Vector3.one;
                
                var hasBounds = false;
                var bounds = new Bounds(Vector3.zero, Vector3.zero);

                var renderers = rootGameObject.GetComponentsInChildren<Renderer>();
                foreach (var childRenderer in renderers)
                {
                    if (hasBounds) {
                        bounds.Encapsulate(childRenderer.bounds);
                    }
                    else {
                        bounds = childRenderer.bounds;
                        hasBounds = true;
                    }
                }
                
                collider.center = bounds.center - rootGameObject.transform.position;
                collider.size = bounds.size;

                rootGameObject.transform.localScale = scale;
            }
        }
     
    }
}