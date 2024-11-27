internal sealed class binder {
    readonly diagbag _diags = new();
    Dictionary<varsym, object> vars;

    public binder(Dictionary<varsym, object> vars) => this.vars = vars;

    public diagbag diags => _diags;

    public bndexpr bindexpr(exprsyn syn) {
        switch(syn.type) {
            case syntype.litexpr:
                return bindlitexpr((litexprsyn)syn);
            case syntype.binexpr:
                return bindbinexpr((binexprsyn)syn);
            case syntype.uniexpr:
                return binduniexpr((uniexprsyn)syn);
            case syntype.parenexpr:
                return bindparenexpr((parenexprsyn)syn);
            case syntype.nameexpr:
                return bindnameexpr((nameexprsyn)syn);
            case syntype.assignexpr:
                return bindassignexpr((assignexprsyn)syn);

            default:
                throw new Exception($"unexpected syntax! got <{syn.type}>");
        }
    }

    bndexpr bindnameexpr(nameexprsyn syn) {
        var name = syn.identtok.text;

        var var = vars.Keys.FirstOrDefault(v => v.name == name);

        if(var == null) {
            _diags.report_undef_name(syn.identtok.span, name);
            return new bndlitexpr(0);
        }

        return new bndvarexpr(var);
    }

    bndexpr bindassignexpr(assignexprsyn syn) {
        var name = syn.identtok.text;
        var bndexpr = bindexpr(syn.expr);

        var existvar = vars.Keys.FirstOrDefault(v => v.name == name);
        if(existvar != null)
            vars.Remove(existvar);

        var var = new varsym(name, bndexpr.cstype);
        vars[var] = null;

        return new bndassignexpr(var, bndexpr);
    }

    bndexpr bindparenexpr(parenexprsyn syn)
        => bindexpr(syn.expr);

    bndexpr bindlitexpr(litexprsyn syn) {
        var val = syn.val ?? 0;

        return new bndlitexpr(val);
    }

    bndexpr bindbinexpr(binexprsyn syn) {
        var bndleft = bindexpr(syn.left);
        var bndright = bindexpr(syn.right);
        var bndoper = bndbinoper.bind(syn.oper.type, bndleft.cstype, bndright.cstype);

        if(bndoper == null) {
            _diags.report_undef_bin_oper(syn.oper.span, syn.oper.text, bndleft.cstype, bndright.cstype);
            return bndleft;
        }

        return new bndbinexpr(bndleft, bndoper, bndright);
    }

    bndexpr binduniexpr(uniexprsyn syn) {
        var bndoand = bindexpr(syn.oand);
        var bndoper = bndunioper.bind(syn.oper.type, bndoand.cstype);

        if(bndoper == null) {
            _diags.report_undef_uni_oper(syn.oper.span, syn.oper.text, bndoand.cstype);
            return bndoand;
        }
        
        return new bnduniexpr(bndoper, bndoand);
    }
}