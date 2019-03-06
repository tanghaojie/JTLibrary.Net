using System;
namespace JT.Check.Rules {
    public interface IRule {
        Guid RuleGuid { get; }
        int RuleID { get; }
        bool RuleCheck { get; }
        string RuleName { get; }
        string RuleRemark { get; }
    }
}