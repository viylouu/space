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
        if(root is bndlitexpr n)
            return n.val;

        if(root is bndvarexpr v) 
            return vars[v.var];

        if(root is bndassignexpr a) {
            var val = evalexpr(a.expr);
            vars[a.var] = val;
            return val;
        }

        if(root is bnduniexpr u) {
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

        if(root is bndbinexpr b) {
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

        throw new Exception($"unexpected node! got <{root.type}>");
    }
}