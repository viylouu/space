public sealed class litexprsyn : exprsyn {
    public litexprsyn(syntok littok) {
        this.littok = littok;
    }

    public override syntype type => syntype.litexpr;

    public syntok littok { get; }

    public override IEnumerable<synnode> getchildren() {
        yield return littok;
    }
}