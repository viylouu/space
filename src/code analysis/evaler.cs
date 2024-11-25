public sealed class evaler {
    readonly exprsyn _root;

    public evaler(exprsyn root) { 
        _root = root;
    }

    public int eval() {
        return evalexpr(_root);
    }

    int evalexpr(exprsyn root) {
        if(root is numexprsyn n)
            return (int)n.numtok.val;

        if(root is binexprsyn b) {
            var left = evalexpr(b.left);
            var right = evalexpr(b.right);

            switch(b.oper.type) {
                case syntype.plustok:
                    return left + right;
                case syntype.minustok:
                    return left - right;
                case syntype.startok:
                    return left * right;
                case syntype.slashtok:
                    return left / right;
                default:
                    throw new Exception($"unexpected binary operator! got <{b.oper.type}>");
            }
        }

        if(root is parenexprsyn p)
            return evalexpr(p.expr);

        throw new Exception($"unexpected node! got <{root.type}>");
    }
}