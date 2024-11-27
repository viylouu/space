using System.Collections.Immutable;

internal sealed class parser {
    readonly ImmutableArray<syntok> _toks;
    readonly diagbag _diags = new();
    int _pos;

    public diagbag diags => _diags;

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

        _toks = toks.ToImmutableArray();
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

        _diags.report_unex_tok(cur.span, cur.type, type);
        return new(type, cur.pos, null, null);
    }

    public syntree parse() {
        var expr = parseexpr();
        var eoftok = matchTok(syntype.eoftok);
        return new syntree(_diags.ToImmutableArray(), expr, eoftok);
    }

    exprsyn parseexpr() {
        return parseassignexpr();
    }

    exprsyn parseassignexpr() {
        if(peek(0).type == syntype.identtok &&
           peek(1).type == syntype.colgreattok) {
            var identtok = nextTok();
            var opertok = nextTok();
            var right = parseassignexpr();
            return new assignexprsyn(identtok, opertok, right);
        }

        return parsebinexpr();
    }

    exprsyn parsebinexpr(int parPrec = 0) {
        exprsyn left;
        var uniprec = cur.type.getunioperprec();

        if(uniprec != 0 && uniprec >= parPrec) {
            var oper = nextTok();
            var oand = parsebinexpr(parPrec);
            left = new uniexprsyn(oper,oand);
        } else
            left = parsepriexpr();

        while(true) {
            var prec = cur.type.getbinoperprec();

            if(prec == 0 || prec <= parPrec)
                break;

            var oper = nextTok();
            var right = parsebinexpr(prec);
            left = new binexprsyn(left,oper,right);
        }

        return left;
    }

    exprsyn parsepriexpr() {
        switch(cur.type) {
            case syntype.oparentok:
                return parseparenexpr();

            case syntype.falsekw:
            case syntype.truekw:
                return parseboollit();

            case syntype.numtok:
                return parsenumlit();

            case syntype.identtok:
            default:
                return parsenameexpr();
        }
    }

    exprsyn parsenumlit() { 
        var numtok = matchTok(syntype.numtok);
        return new litexprsyn(numtok);
    }

    exprsyn parseparenexpr() {
        var left = matchTok(syntype.oparentok);
        var expr = parseexpr();
        var right = matchTok(syntype.cparentok);
        return new parenexprsyn(left, expr, right);
    }

    exprsyn parseboollit() {
        var istrue = cur.type == syntype.truekw;
        var kwtok = matchTok(istrue ? syntype.truekw : syntype.falsekw);
        return new litexprsyn(kwtok, istrue);
    }

    exprsyn parsenameexpr() {
        var ident = matchTok(syntype.identtok);
        return new nameexprsyn(ident);
    }
}