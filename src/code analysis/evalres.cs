public sealed class evalres {
    public evalres(IEnumerable<string> diags, object val) {
        this.diags = diags.ToArray();
        this.val = val;
    }

    public IReadOnlyList<string> diags { get; }
    public object val { get; }
}