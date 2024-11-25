public sealed class litexprsyn : exprsyn {
    public litexprsyn(syntok littok) : this (littok, littok.val) { }

    public litexprsyn(syntok littok, object val) {
        this.littok = littok;
        this.val = val;
    }

    public override syntype type => syntype.litexpr;

    public syntok littok { get; }
    public object val { get; } 

    public override IEnumerable<synnode> getchildren() {
        yield return littok;
    }
}