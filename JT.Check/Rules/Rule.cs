using System;
namespace JT.Check.Rules {
    public class Rule : IRule {
        public Guid RuleGuid { get; } = Guid.Empty;
        public int RuleID { get; } = -1;
        public bool RuleCheck { get; } = false;
        public string RuleName { get; }
        public string RuleRemark { get; }
    }
}
