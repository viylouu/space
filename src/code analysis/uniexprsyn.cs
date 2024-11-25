public sealed class uniexprsyn : exprsyn {
    public uniexprsyn(syntok oper, exprsyn oand) {
        this.oper = oper;
        this.oand = oand;
    }

    public syntok oper { get; }
    public exprsyn oand { get; }

    public override syntype type => syntype.uniexpr;

    public override IEnumerable<synnode> getchildren() {
        yield return oper;
        yield return oand;
    }
}