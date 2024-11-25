public sealed class evalres {
    public evalres(IEnumerable<diag> diags, object val) {
        this.diags = diags.ToArray();
        this.val = val;
    }

    public IReadOnlyList<diag> diags { get; }
    public object val { get; }
}