internal sealed class bndvarexpr : bndexpr {
    public bndvarexpr(string name, Type cstype) {
        this.name = name;
        this.cstype = cstype;
    }

    public override bndnodetype type => throw new NotImplementedException();
    public string name { get; }
    public override Type cstype { get; }
}