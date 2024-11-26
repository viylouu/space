public sealed class nameexprsyn : exprsyn {
    public nameexprsyn(syntok identtok) { 
        this.identtok = identtok;
    }

    public override syntype type => syntype.nameexpr;
    public syntok identtok { get; }

    public override IEnumerable<synnode> getchildren() {
        yield return identtok;
    }
}