using System.Collections.Generic;
using UnityEngine;

public class RBProjectileManager : MonoBehaviour
{
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
        projectileHandlers = new Dictionary<int, HandlerNode>();
    }
    private void Start()
    {
        SceneTriggerManager.OnSceneTransitionEvent += OnSceneTransition;
    }
    private void OnDestroy()
    {
        SceneTriggerManager.OnSceneTransitionEvent -= OnSceneTransition;
    }

    private void OnSceneTransition()
    {
        projectileHandlers.Clear();
    }

    public static RBProjectileHandler RequestHandler(GameObject prefab)
    {
        int id = prefab.GetInstanceID();

        if (projectileHandlers.TryGetValue(id, out HandlerNode node))
        {
            node._count++;
            return node._handler;
        }
        else
        {
            RBProjectileHandler handler = new GameObject(prefab.name + "//SceneHandler").AddComponent<RBProjectileHandler>();
            handler.InitializeHandler(prefab, 5, 50).ProjectileSpeed = 5f;
            HandlerNode newNode = new HandlerNode(handler, 1);
            projectileHandlers.Add(id, newNode);
            return handler;
        }
            
    }

    public struct HandlerNode
    {
        public RBProjectileHandler _handler;
        public int _count;
        public HandlerNode(RBProjectileHandler handler, int count)
        {
            _handler = handler;
            _count = count;
        }
    }

    private static RBProjectileManager _instance;
    private static Dictionary<int, HandlerNode> projectileHandlers;
}
