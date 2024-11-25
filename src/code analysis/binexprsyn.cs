public sealed class binexprsyn : exprsyn {
    public binexprsyn(exprsyn left, syntok oper, exprsyn right) {
        this.left = left;
        this.oper = oper;
        this.right = right;
    }

    public exprsyn left { get; }
    public syntok oper { get; }
    public exprsyn right { get; }

    public override syntype type => syntype.binexpr;

    public override IEnumerable<synnode> getchildren() {
        yield return left;
        yield return oper;
        yield return right;
    }
}