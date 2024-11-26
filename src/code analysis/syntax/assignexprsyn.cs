public sealed class assignexprsyn : exprsyn {
    public assignexprsyn(syntok identtok, syntok eqtok, exprsyn expr) {
        this.identtok = identtok;
        this.eqtok = eqtok;
        this.expr = expr;
    }

    public override syntype type => syntype.assignexpr;
    public syntok identtok { get; } //identifier (not identity)
    public syntok eqtok { get; }
    public exprsyn expr { get; }

    public override IEnumerable<synnode> getchildren() {
        yield return identtok;
        yield return eqtok;
        yield return expr;
    }
}