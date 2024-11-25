internal sealed class parser {
    readonly syntok[] _toks;
    int _pos;
    List<string> _diags = new();

    public IEnumerable<string> diags => _diags;

    public parser(string txt) {
        var toks = new List<syntok>();

        var lex = new lexer(txt);

        syntok tok;
        do {
            tok = lex.lex();

            if(tok.type != syntype.wstok &&
               tok.type != syntype.uktok)
                toks.Add(tok);
        } while(tok.type != syntype.eoftok);

        _toks = toks.ToArray();
        _diags.AddRange(lex.diags);
    }

    syntok peek(int off) {
        var ind = _pos + off;
        if(ind >= _toks.Length)
            return _toks[_toks.Length-1];
        return _toks[ind];
    }

    syntok cur => peek(0);

    syntok nextTok() {
        var _cur = cur;
        _pos++;
        return _cur;
    }

    syntok matchTok(syntype type) {
        if(cur.type == type)
            return nextTok();

        _diags.Add($"err: unexpected token: <{cur.type}>, expected type <{type}>");
        return new(type, cur.pos, null, null);
    }

    public syntree parse() {
        var expr = parseexpr();
        var eoftok = matchTok(syntype.eoftok);
        return new syntree(_diags, expr, eoftok);
    }

    exprsyn parseexpr(int parPrec = 0) {
        var left = parsepriexpr();

        while(true) {
            var prec = getbinoperprec(cur.type);

            if(prec == 0 || prec <= parPrec)
                break;

            var opertok = nextTok();
            var right = parseexpr(prec);
            left = new binexprsyn(left,opertok,right);
        }

        return left;
    }

    static int getbinoperprec(syntype type) {
        switch(type) {
            case syntype.plustok:
            case syntype.minustok:
                return 1;

            case syntype.startok:
            case syntype.slashtok:
                return 2;

            default:
                return 0;
        }
    }

    exprsyn parsepriexpr() {
        if(cur.type == syntype.oparentok) {
            var left = nextTok();
            var expr = parseexpr();
            var right = matchTok(syntype.cparentok);
            return new parenexprsyn(left, expr, right); 
        }

        var numTok = matchTok(syntype.numtok);
        return new litexprsyn(numTok);
    }
}
