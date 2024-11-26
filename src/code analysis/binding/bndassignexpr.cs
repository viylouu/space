internal sealed class bndassignexpr : bndexpr {
    public bndassignexpr(varsym var, bndexpr expr) {
        this.var = var;
        this.expr = expr;
    }

    public override bndnodetype type => bndnodetype.assignexpr;
    public override Type cstype => expr.cstype;

    public varsym var { get; }
    public bndexpr expr { get; }
}