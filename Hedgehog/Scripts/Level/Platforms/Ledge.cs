﻿using Hedgehog.Core.Actors;
using Hedgehog.Core.Triggers;
using Hedgehog.Core.Utils;
using UnityEngine;

namespace Hedgehog.Level.Platforms
{
    /// <summary>
    /// Turns a platform into a ledge where a controller can only collide with its top side.
    /// </summary>
    [RequireComponent(typeof(PlatformTrigger))]
    public class Ledge : MonoBehaviour
    {
        private PlatformTrigger _trigger;

        public void Awake()
        {
            _trigger = GetComponent<PlatformTrigger>();
        }
        public void OnEnable()
        {
            _trigger.CollisionPredicates.Add(CollisionPredicate);
        }

        public void OnDisable()
        {
            _trigger.CollisionPredicates.Remove(CollisionPredicate);
        }

        // The platform can be collided with if the player is checking its bottom side and
        // the result of the check did not stop where it started.
        public static bool CollisionPredicate(TerrainCastHit hit)
        {
            if(hit.Source == null) 
                return (hit.Side & TerrainSide.Bottom) > 0;
            
            // Check must be coming from player's bottom side and be close to the top
            // of the platform
            return (hit.Side & TerrainSide.Bottom) > 0 && hit.Hit.fraction > 0.0f;
        }
    }
}
