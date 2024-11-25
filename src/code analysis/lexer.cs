internal sealed class lexer {
    readonly string _txt;
    int _pos;
    List<string> _diags = new();

    public lexer(string txt) {
        _txt = txt;
    }

    public IEnumerable<string> diags => _diags;

    char cur {
        get {
            if(_pos >= _txt.Length)
                return '\0';

            return _txt[_pos];
        }
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
                _diags.Add($"err: the num {_txt} is not a valid i32");

            return new(syntype.littok, start, text, val);
        }

        if(char.IsWhiteSpace(cur)) {
            var start = _pos;

            while(char.IsWhiteSpace(cur))
                next();

            var len = _pos - start;
            var text = _txt.Substring(start, len);

            return new(syntype.wstok, start, text, null);
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
        }

        _diags.Add($"err: unknown char in input: '{cur}'");
        return new(syntype.uktok, _pos++, _txt.Substring(_pos - 1, 1), null);
    }
}