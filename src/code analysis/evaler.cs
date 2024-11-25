internal sealed class evaler {
    readonly bndexpr _root;

    public evaler(bndexpr root) { 
        _root = root;
    }

    public int eval() {
        return evalexpr(_root);
    }

    int evalexpr(bndexpr root) {
        if(root is bndlitexpr n)
            return (int)n.val;

        if(root is bnduniexpr u) {
            var oand = evalexpr(u.oand);

            switch(u.oper) {
                case bnduniopertype.neg:
                    return -oand;
                default:
                    throw new Exception($"unexpected unary operator! got <{u.oper}>");
            }
        }

        if(root is bndbinexpr b) {
            var left = evalexpr(b.left);
            var right = evalexpr(b.right);

            switch(b.oper) {
                case bndbinopertype.add:
                    return left + right;
                case bndbinopertype.sub:
                    return left - right;
                case bndbinopertype.mul:
                    return left * right;
                case bndbinopertype.div:
                    return left / right;
                default:
                    throw new Exception($"unexpected binary operator! got <{b.oper}>");
            }
        }

        throw new Exception($"unexpected node! got <{root.type}>");
    }
}