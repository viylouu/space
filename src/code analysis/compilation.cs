public sealed class compilation {
    public compilation(syntree syn) { 
        this.syn = syn;
    }

    public syntree syn { get; }

    public evalres eval() {
        var binder = new binder();
        var bndexpr = binder.bindexpr(syn.root);

        var diags = syn.diags.Concat(binder.diags).ToArray();
        if(diags.Any())
            return new evalres(diags, null);

        var evaler = new evaler(bndexpr);
        var val = evaler.eval();
        return new evalres(Array.Empty<string>(), val);
    }
}