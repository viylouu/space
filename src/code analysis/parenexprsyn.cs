sealed class parenexprsyn : exprsyn {
    public parenexprsyn(syntok openParen, exprsyn expr, syntok closeParen) {
        this.openParen = openParen;
        this.expr = expr;
        this.closeParen = closeParen;
    }

    public syntok openParen { get; }
    public exprsyn expr { get; }
    public syntok closeParen { get; }

    public override syntype type => syntype.parenexpr;

    public override IEnumerable<synnode> getchildren() {
        yield return openParen;
        yield return expr;
        yield return closeParen;
    }
}