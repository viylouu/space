internal sealed class bnduniexpr : bndexpr {
    public bnduniexpr(bnduniopertype oper, bndexpr oand) { 
        this.oper = oper;
        this.oand = oand;
    }

    public override bndnodetype type => bndnodetype.uniexpr;
    public override Type cstype => oand.cstype;
    public bnduniopertype oper { get; }
    public bndexpr oand { get; }
}