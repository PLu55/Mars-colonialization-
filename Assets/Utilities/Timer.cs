using System;
 
namespace PLu.Utilities
{
    public abstract class Timer 
    {
        protected float initialTime;
        public float Time { get; set; }
        public float TimePast => initialTime - Time;
        public bool IsRunning { get; protected set; }

        public float Progress => Time / initialTime;

        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate { };

        protected Timer(float initialTime) {
            this.initialTime = initialTime;
            IsRunning = false;
        }

        public virtual void Start() 
        {
            Time = initialTime;

            if (!IsRunning) {
                IsRunning = true;
                OnTimerStart.Invoke();
            }
        }

        public void Stop() 
        {
            if (IsRunning) 
            {
                IsRunning = false;
                OnTimerStop.Invoke();
            }
        }

        public void Resume() => IsRunning = true;
        public void Pause() => IsRunning = false;

        public abstract void Tick(float deltaTime);
    }

    public class CountdownTimer : Timer 
    {
        public CountdownTimer(float value, bool start = false) : base(value)
        {
            if (start) 
            {
                Start();
            }
         }

        public override void Tick(float deltaTime) 
        {
            if (IsRunning && Time > 0) 
            {   
                Time -= deltaTime;
            }

            if (IsRunning && Time <= 0) {
                Stop();
            }
        }

        public bool IsFinished => Time <= 0;

        public void Reset() => Time = initialTime;

        public void Reset(float newTime) 
        {
            initialTime = newTime;
            Reset();
        }
    }
    public class RepeatTimer : Timer 
    {

        public Action<float> OnTimerRepeat = delegate { };
        public RepeatTimer(float value, bool start = false) : base(value) 
        {
            if (start) 
            {
                Start();
            }
        }

        public override void Start()
        {
            OnTimerRepeat.Invoke(0f);
            base.Start();
        }
        public override void Tick(float deltaTime) 
        {
            if (IsRunning) {
                Time -= deltaTime;

                if (Time <= 0) {
                    OnTimerRepeat.Invoke(initialTime - Time);
                    Time = initialTime;
                    
                }
            }
        }
    }
    public class StopwatchTimer : Timer 
    {
        public StopwatchTimer() : base(0) { }

        public override void Tick(float deltaTime) 
        {
            if (IsRunning) 
            {
                Time += deltaTime;
            }
        }

        public void Reset() => Time = 0;

        public float GetTime() => Time;
    }
}
