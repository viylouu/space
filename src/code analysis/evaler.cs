internal sealed class evaler {
    readonly bndexpr _root;
    readonly Dictionary<varsym, object> vars;

    public evaler(bndexpr root, Dictionary<varsym, object> vars) { 
        _root = root;
        this.vars = vars;
    }

    public object eval() {
        return evalexpr(_root);
    }

    object evalexpr(bndexpr root) {
        switch(root.type) {
            case bndnodetype.litexpr:
                return evallitexpr((bndlitexpr)root);
            case bndnodetype.varexpr:
                return evalvarexpr((bndvarexpr)root);
            case bndnodetype.assignexpr:
                return evalassignexpr((bndassignexpr)root);
            case bndnodetype.uniexpr:
                return evaluniexpr((bnduniexpr)root);
            case bndnodetype.binexpr:
                return evalbinexpr((bndbinexpr)root);
            default:
                throw new Exception($"unexpected node! got <{root.type}>");
        }
    }

    object evalbinexpr(bndbinexpr b) {
        var left = evalexpr(b.left);
        var right = evalexpr(b.right);

        switch(b.oper.type) {
            case bndbinopertype.add:
                return (int)left + (int)right;
            case bndbinopertype.sub:
                return (int)left - (int)right;
            case bndbinopertype.mul:
                return (int)left * (int)right;
            case bndbinopertype.div:
                return (int)left / (int)right;
            case bndbinopertype.logand:
                return (bool)left && (bool)right;
            case bndbinopertype.logor:
                return (bool)left || (bool)right;
            case bndbinopertype.eq:
                return Equals(left, right);
            case bndbinopertype.noteq:
                return !Equals(left, right);
            default:
                throw new Exception($"unexpected binary operator! got <{b.oper}>");
        }
    }

    object evaluniexpr(bnduniexpr u) {
        var oand = evalexpr(u.oand);

        switch(u.oper.type) {
            case bnduniopertype.neg:
                return -(int)oand;
            case bnduniopertype.logneg:
                return !(bool)oand;
            default:
                throw new Exception($"unexpected unary operator! got <{u.oper}>");
        }
    }

    object evalassignexpr(bndassignexpr a) {
        var val = evalexpr(a.expr);
        vars[a.var] = val;
        return val;
    }

    object evalvarexpr(bndvarexpr v) => vars[v.var];
    object evallitexpr(bndlitexpr n) => n.val;
}