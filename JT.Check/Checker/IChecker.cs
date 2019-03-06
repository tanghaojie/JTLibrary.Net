using JT.Check.Data;

namespace JT.Check.Checker {
    public interface IChecker {
        string Name { get; }
        void Check(IData data);
        //void Append
    }
}
