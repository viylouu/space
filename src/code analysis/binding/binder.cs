internal sealed class binder {
    readonly diagbag _diags = new();

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
                return bindexpr(((parenexprsyn)syn).expr);

            default:
                throw new Exception($"unexpected syntax! got <{syn.type}>");
        }
    }

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