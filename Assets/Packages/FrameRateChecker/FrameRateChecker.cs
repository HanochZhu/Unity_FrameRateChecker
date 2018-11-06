﻿using UnityEngine;

public class FrameRateChecker : SingletonMonoBehaviour<FrameRateChecker>
{
    // NOTE:
    // These settings are important info.
    // Application.targetFrameRate setting is ignored in Editor.
    // 
    // - QualitySettings.vSyncCount
    // - Application.targetFrameRate

    #region Field

    protected float frameCount;
    protected float elapsedTime;

    [SerializeField, DisableInInspector]
    private float fps;

    [SerializeField, DisableInInspector]
    private float fpsAvg;
    
    [SerializeField, DisableInInspector]
    private float fpsMin;
    
    [SerializeField, DisableInInspector]
    private float fpsMax;

    #endregion Field

    #region Property

    public bool IsInitialized 
    {
        protected set; get;
    }

    public float Fps
    {
        get { return this.fps; }
    }

    public float FpsAvg
    {
        get { return this.fpsAvg; }
    }

    public float FpsMin
    {
        get { return this.fpsMin; }
    }

    public float FpsMax
    {
        get { return this.fpsMax; }
    }

    #endregion Property

    #region Method

    protected virtual void Update()
    {
        // NOTE:
        // Maybe this is not strict.
        // this.fps = 1 / Time.deltaTime;

        this.frameCount  += 1;
        this.elapsedTime += Time.deltaTime;

        if (this.elapsedTime > 1f)
        {
            this.fps = this.frameCount / this.elapsedTime;
            this.frameCount  = 0;
            this.elapsedTime = 0;

            if (this.IsInitialized)
            {
                this.fpsAvg = (this.fpsAvg + this.fps) / 2;

                if (this.fps < this.fpsMin)
                {
                    this.fpsMin = this.fps;
                }

                if (this.fpsMax < this.fps)
                {
                    this.fpsMax = this.fps;
                }
            }
            else
            {
                this.IsInitialized = true;

                this.fpsAvg = this.fps;
                this.fpsMin = this.fps;
                this.fpsMax = this.fps;
            }
        }
    }

    public virtual void Reset() 
    {
        this.frameCount  = 0;
        this.elapsedTime = 0;

        this.IsInitialized = false;

        this.fps    = 0;
        this.fpsAvg = 0;
        this.fpsMin = 0;
        this.fpsMax = 0;
    }

    #endregion Method
}