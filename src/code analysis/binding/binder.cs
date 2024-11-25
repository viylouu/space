internal sealed class binder {
    readonly List<string> _diags = new();

    public IEnumerable<string> diags => _diags;

    public bndexpr bindexpr(exprsyn syn) {
        switch(syn.type) {
            case syntype.litexpr:
                return bindlitexpr((litexprsyn)syn);
            case syntype.binexpr:
                return bindbinexpr((binexprsyn)syn);
            case syntype.uniexpr:
                return binduniexpr((uniexprsyn)syn);
            
            default:
                throw new Exception($"unexpected syntax! got <{syn.type}>");
        }
    }

    bndexpr bindlitexpr(litexprsyn syn) {
        var val = syn.littok.val as int? ?? 0;

       return new bndlitexpr(val);
    }

    bndexpr bindbinexpr(binexprsyn syn) {
        var bndleft = bindexpr(syn.left);
        var bndright = bindexpr(syn.right);
        var bndoper = bindbinopertype(syn.oper.type, bndleft.cstype, bndright.cstype);

        if(bndoper == null) {
            _diags.Add($"binary operator '{syn.oper.txt}' is not defined for types <{bndleft.type}> and <{bndright.type}>");
            return bndleft;
        }

        return new bndbinexpr(bndleft, bndoper.Value, bndright);
    }

    bndexpr binduniexpr(uniexprsyn syn) {
        var bndoand = bindexpr(syn.oand);
        var bndoper = binduniopertype(syn.oper.type, bndoand.cstype);

        if(bndoper == null) {
            _diags.Add($"unary operator '{syn.oper.txt}' is not defined for type <{bndoand.type}>");
            return bndoand;
        }
        
        return new bnduniexpr(bndoper.Value, bndoand);
    }

    bnduniopertype? binduniopertype(syntype type, Type oandcstype) {
        if(oandcstype != typeof(int))
            return null;

        switch(type) {
            case syntype.minustok:
                return bnduniopertype.neg;
            default:
                throw new Exception($"unexpected unary operator! got <{type}>");
        }
    }

    bndbinopertype? bindbinopertype(syntype type, Type leftcstype, Type rightcstype) {
        if(leftcstype != typeof(int) || rightcstype != typeof(int))
            return null;

        switch(type) {
            case syntype.plustok:
                return bndbinopertype.add;
            case syntype.minustok:
                return bndbinopertype.sub;
            case syntype.startok:
                return bndbinopertype.mul;
            case syntype.slashtok:
                return bndbinopertype.div;
            default:
                throw new Exception($"unexpected binary operator! got <{type}>");
        }
    }
}