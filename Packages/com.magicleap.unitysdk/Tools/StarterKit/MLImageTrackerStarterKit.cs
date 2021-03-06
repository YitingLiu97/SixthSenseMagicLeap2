// %BANNER_BEGIN%
// ---------------------------------------------------------------------
// %COPYRIGHT_BEGIN%
//
// Copyright (c) 2019-present, Magic Leap, Inc. All Rights Reserved.
// Use of this file is governed by the Developer Agreement, located
// here: https://auth.magicleap.com/terms/developer
//
// %COPYRIGHT_END%
// ---------------------------------------------------------------------
// %BANNER_END%

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

namespace MagicLeap.Core.StarterKit
{
    /// <summary>
    /// Starter kit class for practical use of MLImageTracker.
    /// </summary>
    public static class MLImageTrackerStarterKit
    {
        /// <summary>
        /// Class used to hold an image target and all of it's status callbacks.
        /// </summary>
        public class MLImageTargetStarterKit
        {
            #if PLATFORM_LUMIN
            public delegate void StatusUpdate(MLImageTracker.Target target, MLImageTracker.Target.Result result);

            public event StatusUpdate OnFound;
            public event StatusUpdate OnLost;
            public event StatusUpdate OnUpdated;
            #endif

            public void InitializeWithTarget(MLImageTracker.Target target)
            {
                if(Target == null)
                {
                    Target = target;
                    Status = MLImageTracker.Target.TrackingStatus.NotTracked;
                }
            }

            public MLImageTracker.Target.TrackingStatus Status
            {
                get;
                private set;
            }

            public MLImageTracker.Target Target
            {
                get;
                set;
            }

            #if PLATFORM_LUMIN
            /// <summary>
            /// Calls the appropriate StatusUpdate events for this target.
            /// </summary>
            public void HandleStatusUpdates(MLImageTracker.Target target, MLImageTracker.Target.Result result)
            {
                if (result.Status != Status)
                {
                    Status = result.Status;

                    if (Status == MLImageTracker.Target.TrackingStatus.Tracked || Status == MLImageTracker.Target.TrackingStatus.Unreliable)
                    {
                        OnFound?.Invoke(target, result);
                    }

                    else
                    {
                        OnLost?.Invoke(target, result);
                    }
                }
                else
                {
                    OnUpdated?.Invoke(target, result);
                }
            }
            #endif
        }

        /// <summary>
        /// Used for enabling the image tracker without resetting the API.
        /// </summary>
        public static MLResult Enable()
        {
#if PLATFORM_LUMIN
            _result = MLImageTracker.Enable();
#endif

            return _result;
        }

        /// <summary>
        /// Used for disabling the image tracker without resetting the API.
        /// </summary>
        public static MLResult Disable()
        {
#if PLATFORM_LUMIN
            _result = MLImageTracker.Disable();
#endif

            return _result;
        }

        /// <summary>
        /// Used for getting the current tracker status.
        /// </summary>
        public static bool GetTrackerStatus()
        {
#if PLATFORM_LUMIN
            return MLImageTracker.GetTrackerStatus();
#else
            return false;
#endif
        }

#pragma warning disable 649
        private static MLResult _result;
        #pragma warning restore 649

        /// <summary>
        /// Starts up MLImageTracker.
        /// Invokes the callback after privleges have been succesfully requested and MLImageTracker is started.
        /// </summary>
        public static MLResult Start()
        {
            #if PLATFORM_LUMIN
            _result = MLPrivileges.RequestPrivileges(MLPrivileges.Id.CameraCapture);
            if (_result.Result != MLResult.Code.PrivilegeGranted)
            {
                Debug.LogErrorFormat("Error: MLImageTrackerStarterKit failed requesting privileges. Reason: {0}", _result);
                return _result;
            }
            #endif

            return _result;
        }

        /// <summary>
        /// Stop.
        /// </summary>
        public static void Stop()
        {
        }

        #if PLATFORM_LUMIN
        /// <summary>
        // Adds an image target to be tracked.
        /// </summary>
        public static MLImageTargetStarterKit AddTarget(string id, Texture2D texture, float longerDimension, MLImageTracker.Target.OnImageResultDelegate callback, bool isStationary = false)
        {
            MLImageTargetStarterKit newTargetStarterKit = new MLImageTargetStarterKit();
            callback += newTargetStarterKit.HandleStatusUpdates;

            MLImageTracker.Target newTarget = MLImageTracker.AddTarget(id, texture, longerDimension, callback, isStationary);

            if (newTarget == null)
            {
                Debug.LogErrorFormat("MLImageTrackerStarterKit.AddTarget was unable to create the new image target with id: {0}.", id);
                return null;
            }

            newTargetStarterKit.InitializeWithTarget(newTarget);
            return newTargetStarterKit;
        }
        #endif

        /// <summary>
        // Remove an image target from being tracked.
        /// </summary>
        public static bool RemoveTarget(string id)
        {
#if PLATFORM_LUMIN
            return MLImageTracker.RemoveTarget(id);
#else
            return false;
#endif
        }
    }
}
