using JT.Check.Checker;
using JT.Check.Data;
using JT.Check.Rules;
namespace JT.Check.Result {
    interface IResult {
        IRule Rule { get; }
        IData Data { get; }
        IChecker Checker { get; }
        IResult InnerResult { get; }
        string Name { get; }
    }
}