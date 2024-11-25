internal sealed class bndbinexpr : bndexpr {
    public bndbinexpr(bndexpr left, bndbinoper oper, bndexpr right) { 
        this.left = left;
        this.oper = oper;
        this.right = right;
    }

    public override bndnodetype type => bndnodetype.binexpr;
    public override Type cstype => oper.rescstype;
    public bndexpr left { get; }
    public bndbinoper oper { get; }
    public bndexpr right { get; }
}