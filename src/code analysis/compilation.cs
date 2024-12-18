﻿using System.Collections.Immutable;

public sealed class compilation {
    public compilation(syntree syn) { 
        this.syn = syn;
    }

    public syntree syn { get; }

    public evalres eval(Dictionary<varsym, object> vars) {
        var binder = new binder(vars);
        var bndexpr = binder.bindexpr(syn.root);

        var diags = syn.diags.Concat(binder.diags).ToImmutableArray();
        if(diags.Any())
            return new evalres(diags, null);

        var evaler = new evaler(bndexpr, vars);
        var val = evaler.eval();
        return new evalres(ImmutableArray<diag>.Empty, val);
    }
}