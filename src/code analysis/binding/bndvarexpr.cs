internal sealed class bndvarexpr : bndexpr {
    public bndvarexpr(varsym var) {
        this.var = var;
    }

    public override bndnodetype type => bndnodetype.varexpr;
    public varsym var { get; }
    public override Type cstype => var.cstype;
}