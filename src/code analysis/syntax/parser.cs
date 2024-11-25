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
        exprsyn left;
        var uniprec = cur.type.getunioperprec();

        //had to mess around to make it not allow more than 1 negative because i said so
        if(uniprec != 0 && uniprec > parPrec) {
            var oper = nextTok();
            var oand = parsepriexpr();
            left = new uniexprsyn(oper,oand);
        } else
            left = parsepriexpr();

        while(true) {
            var prec = cur.type.getbinoperprec();

            if(prec == 0 || prec <= parPrec)
                break;

            var oper = nextTok();
            var right = parseexpr(prec);
            left = new binexprsyn(left,oper,right);
        }

        return left;
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
