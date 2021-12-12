using System.Diagnostics;

namespace EngineBalloon.Physics
{
    internal static class Timer
    {
        private const float MillisecondsFactor = 0.001f;

        private static long _lastElapsedMilliseconds;

        private static float _lastFixedUpdate;

        static Timer()
        {
            Stopwatch = new Stopwatch();
            FixedUpdateTime = 1.0f / 30.0f;
        }

        public static Stopwatch Stopwatch { get; private set; }

        public static float DeltaTime { get; private set; }

        public static int FrameCount { get; private set; }

        public static float FixedUpdateTime { get; set; }

        public static void Run()
        {
            Stopwatch.Start();
        }

        public static void Reset()
        {
            _lastFixedUpdate = default;
            _lastElapsedMilliseconds = default;
            DeltaTime = default;
            FrameCount = default;
            Stopwatch.Reset();
        }

        public static void Update()
        {
            DeltaTime = (Stopwatch.ElapsedMilliseconds - _lastElapsedMilliseconds) * 0.001f;
            _lastElapsedMilliseconds = Stopwatch.ElapsedMilliseconds;
            FrameCount = (int)(1.0f / DeltaTime);
        }

        public static bool IsFixedUpdate()
        {
            if (_lastFixedUpdate < Stopwatch.ElapsedMilliseconds * MillisecondsFactor)
            {
                _lastFixedUpdate = Stopwatch.ElapsedMilliseconds * MillisecondsFactor;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
