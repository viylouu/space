internal sealed class lexer {
    readonly string _txt;
    int _pos;
    diagbag _diags = new();

    public lexer(string txt) {
        _txt = txt;
    }

    public diagbag diags => _diags;

    char cur => peek(0);

    char ahead => peek(1);

    char peek(int off) {
        var ind = _pos+off;

        if(ind >= _txt.Length)
                return '\0';

        return _txt[_pos];
    }

    void next() => _pos++;

    public syntok lex() {
        if(_pos >= _txt.Length)
            return new(syntype.eoftok, _pos, "\0", null);

        if(char.IsDigit(cur)) {
            var start = _pos;

            while(char.IsDigit(cur))
                next();

            var len = _pos - start;
            var text = _txt.Substring(start, len);

            if(!int.TryParse(text, out var val))
                _diags.report_invalid_num(new txtspan(start, len), _txt, typeof(int));

            return new(syntype.numtok, start, text, val);
        }

        if(char.IsWhiteSpace(cur)) {
            var start = _pos;

            while(char.IsWhiteSpace(cur))
                next();

            var len = _pos - start;
            var text = _txt.Substring(start, len);

            return new(syntype.wstok, start, text, null);
        }

        if(char.IsLetter(cur)) {
            var start = _pos;

            while(char.IsLetter(cur))
                next();

            var len = _pos - start;
            var text = _txt.Substring(start, len);

            var type = synfacts.getkwtype(text);

            return new(type, start, text, null);
        }

        switch(cur) {
            case '+':
                return new(syntype.plustok, _pos++, "+", null);
            case '-':
                return new(syntype.minustok, _pos++, "-", null);
            case '*':
                return new(syntype.startok, _pos++, "*", null);
            case '/':
                return new(syntype.slashtok, _pos++, "/", null);
            case '(':
                return new(syntype.oparentok, _pos++, "(", null);
            case ')':
                return new(syntype.cparentok, _pos++, ")", null);

            case '&':
                if(ahead == '&')
                    return new(syntype.ampamptok, _pos += 2, "&&", null);
                return new(syntype.amptok, _pos++, "&", null);
            case '|':
                if(ahead == '|')
                    return new(syntype.barbartok, _pos += 2, "||", null);
                return new(syntype.bartok, _pos++, "|", null);
            case '=':
                if(ahead == '=')
                    return new(syntype.eqeqtok, _pos += 2, "==", null);
                break;
            case '!':
                if(ahead == '=')
                    return new(syntype.bangeqtok, _pos += 2, "!=", null);
                return new(syntype.bangtok, _pos++, "!", null);
        }

        _diags.report_bad_char(_pos, cur);
        return new(syntype.uktok, _pos++, _txt.Substring(_pos - 1, 1), null);
    }
}