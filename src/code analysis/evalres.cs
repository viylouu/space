using System.Collections.Immutable;

public sealed class evalres {
    public evalres(ImmutableArray<diag> diags, object val) {
        this.diags = diags;
        this.val = val;
    }

    public ImmutableArray<diag> diags { get; }
    public object val { get; }
} 