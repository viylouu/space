sealed class numexprsyn : exprsyn {
    public numexprsyn(syntok numtok) {
        this.numtok = numtok;
    }

    public override syntype type => syntype.numexpr;

    public syntok numtok { get; }

    public override IEnumerable<synnode> getchildren() {
        yield return numtok;
    }
}