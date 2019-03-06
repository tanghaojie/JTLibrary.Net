using System;
namespace JT.Check.Level {
    public partial class Level {
        public Level(int level, string levelName, string displayName) {
            Value = level;
            Name = string.Intern(levelName ?? throw new ArgumentNullException("levelName"));
            Display = displayName ?? throw new ArgumentNullException("displayName");
        }
        public Level(int level, string levelName) : this(level, levelName, levelName) { }
    }
    public partial class Level {
        public string Name { get; }
        public int Value { get; }
        public string Display { get; }
    }
    public partial class Level {
        public readonly static Level Off = new Level(int.MaxValue, "OFF");
        public readonly static Level Log4Net_Debug = new Level(120000, "log4net:DEBUG");
        public readonly static Level Emergency = new Level(120000, "EMERGENCY");
        public readonly static Level Fatal = new Level(110000, "FATAL");
        public readonly static Level Alert = new Level(100000, "ALERT");
        public readonly static Level Critical = new Level(90000, "CRITICAL");
        public readonly static Level Severe = new Level(80000, "SEVERE");
        public readonly static Level Error = new Level(70000, "ERROR");
        public readonly static Level Warn = new Level(60000, "WARN");
        public readonly static Level Notice = new Level(50000, "NOTICE");
        public readonly static Level Info = new Level(40000, "INFO");
        public readonly static Level Debug = new Level(30000, "DEBUG");
        public readonly static Level Fine = new Level(30000, "FINE");
        public readonly static Level Trace = new Level(20000, "TRACE");
        public readonly static Level Finer = new Level(20000, "FINER");
        public readonly static Level Verbose = new Level(10000, "VERBOSE");
        public readonly static Level Finest = new Level(10000, "FINEST");
        public readonly static Level All = new Level(int.MinValue, "ALL");
    }
}
