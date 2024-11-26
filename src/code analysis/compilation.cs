public sealed class compilation {
    public compilation(syntree syn) { 
        this.syn = syn;
    }

    public syntree syn { get; }

    public evalres eval(Dictionary<string, object> vars) {
        var binder = new binder(vars);
        var bndexpr = binder.bindexpr(syn.root);

        var diags = syn.diags.Concat(binder.diags).ToArray();
        if(diags.Any())
            return new evalres(diags, null);

        var evaler = new evaler(bndexpr, vars);
        var val = evaler.eval();
        return new evalres(Array.Empty<diag>(), val);
    }
}