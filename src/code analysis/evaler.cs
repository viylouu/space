internal sealed class evaler {
    readonly bndexpr _root;

    public evaler(bndexpr root) { 
        _root = root;
    }

    public object eval() {
        return evalexpr(_root);
    }

    object evalexpr(bndexpr root) {
        if(root is bndlitexpr n)
            return n.val;

        if(root is bnduniexpr u) {
            var oand = evalexpr(u.oand);

            switch(u.oper) {
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

            switch(b.oper) {
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
                default:
                    throw new Exception($"unexpected binary operator! got <{b.oper}>");
            }
        }

        throw new Exception($"unexpected node! got <{root.type}>");
    }
}