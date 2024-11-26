internal sealed class binder {
    readonly diagbag _diags = new();
    Dictionary<string, object> vars;

    public binder(Dictionary<string, object> vars) => this.vars = vars;

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
        var name = syn.identtok.txt;

        if(!vars.TryGetValue(name, out var val)) {
            _diags.report_undef_name(syn.identtok.span, name);
            return new bndlitexpr(0);
        }

        var type = val.GetType();
        return new bndvarexpr(name, type);
    }

    bndexpr bindassignexpr(assignexprsyn syn) {
        var name = syn.identtok.txt;
        var bndexpr = bindexpr(syn.expr);

        var defval =
            bndexpr.cstype == typeof(int)
            ? (object)0
            : bndexpr.cstype == typeof(bool)
            ? false
            : null;

        if(defval == null)
            throw new Exception($"Unsupported var type <{bndexpr.cstype}>");

        vars[name] = defval;

        return new bndassignexpr(name, bndexpr);
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
            _diags.report_undef_bin_oper(syn.oper.span, syn.oper.txt, bndleft.cstype, bndright.cstype);
            return bndleft;
        }

        return new bndbinexpr(bndleft, bndoper, bndright);
    }

    bndexpr binduniexpr(uniexprsyn syn) {
        var bndoand = bindexpr(syn.oand);
        var bndoper = bndunioper.bind(syn.oper.type, bndoand.cstype);

        if(bndoper == null) {
            _diags.report_undef_uni_oper(syn.oper.span, syn.oper.txt, bndoand.cstype);
            return bndoand;
        }
        
        return new bnduniexpr(bndoper, bndoand);
    }
}