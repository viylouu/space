internal sealed class bndbinexpr : bndexpr {
    public bndbinexpr(bndexpr left, bndbinopertype oper, bndexpr right) { 
        this.left = left;
        this.oper = oper;
        this.right = right;
    }

    public override bndnodetype type => bndnodetype.uniexpr;
    public override Type cstype => left.cstype;
    public bndexpr left { get; }
    public bndbinopertype oper { get; }
    public bndexpr right { get; }
}