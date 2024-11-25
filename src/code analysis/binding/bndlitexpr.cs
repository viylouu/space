internal sealed class bndlitexpr : bndexpr {
    public bndlitexpr(object val) { 
        this.val = val;
    }

    public override bndnodetype type => bndnodetype.litexpr;
    public override Type cstype => val.GetType();

    public object val { get; }
}