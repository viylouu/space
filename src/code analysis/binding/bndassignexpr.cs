internal sealed class bndassignexpr : bndexpr {
    public bndassignexpr(string name, bndexpr expr) {
        this.name = name;
        this.expr = expr;
    }

    public override bndnodetype type => bndnodetype.assignexpr;
    public override Type cstype => expr.cstype;

    public string name { get; }
    public bndexpr expr { get; }
}