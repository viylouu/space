internal sealed class lexer {
    readonly string _text;
    readonly diagbag _diags = new();
    int _pos;

    int _start;
    syntype _type;
    object _val;

    public lexer(string text) {
        _text = text;
    }

    public diagbag diags => _diags;

    char cur => peek(0);

    char ahead => peek(1);

    char peek(int off) {
        var ind = _pos+off;

        if(ind >= _text.Length)
            return '\0';

        return _text[ind];
    }

    public syntok lex() {
        _start = _pos;
        _type = syntype.uktok;
        _val = null;

        switch(cur) {
            case '\0':
                _type = syntype.eoftok;
                break;
            case '+':
                _type = syntype.plustok;
                _pos++;
                break;
            case '-':
                _type = syntype.minustok;
                _pos++;
                break;
            case '*':
                _type = syntype.startok;
                _pos++;
                break;
            case '/':
                _type = syntype.slashtok;
                _pos++;
                break;
            case '(':
                _type = syntype.oparentok;
                _pos++;
                break;
            case ')':
                _type = syntype.cparentok;
                _pos++;
                break;

            case '&':
                if(ahead == '&') {
                    _type = syntype.ampamptok;
                    _pos+=2;
                    break;
                }
                _type = syntype.amptok;
                _pos++;
                break;
            case '|':
                if(ahead == '|') {
                    _type = syntype.barbartok;
                    _pos+=2;
                    break;
                }
                _type = syntype.bartok;
                _pos++;
                break;
            case '=':
                if(ahead == '=') {
                    _type = syntype.eqeqtok;
                    _pos+=2;
                    break;
                }
                break;
            case '!':
                if(ahead == '=') {
                    _type = syntype.bangeqtok;
                    _pos+=2;
                    break;
                }
                _type = syntype.bangtok;
                _pos++;
                break;
            case ':':
                if(ahead == '>') {
                    _type = syntype.colgreattok;
                    _pos+=2;
                    break;
                } else if(ahead == ':') {
                    _type = syntype.colcoltok;
                    _pos += 2;
                    break;
                }
                _type = syntype.coltok;  
                _pos++;
                break;
            case '0': case '1': case '2': case '3': case '4':
            case '5': case '6': case '7': case '8': case '9':
                readnumtok();
                break;
            case ' ':
            case '\t':
            case '\n':
            case '\r':
                readws();
                break;
            default:
                if(char.IsLetter(cur))
                    readidentorkw();
                else if(char.IsWhiteSpace(cur))
                    readws();
                else {
                    _diags.report_bad_char(_pos, cur);
                    _pos++;
                }
                break;
        }

        var text = synfacts.gettext(_type);

        var len = _pos - _start;

        if(text == null)
            text = _text.Substring(_start, len);

        return new(_type, _start, text, _val);
    }

    void readidentorkw() {
        while(char.IsLetter(cur))
            _pos++;

        var len = _pos - _start;
        var text = _text.Substring(_start, len);
        _type = synfacts.getkwtype(text);
    }

    private void readws() {
        while(char.IsWhiteSpace(cur))
            _pos++;

        _type = syntype.wstok;
    }

    void readnumtok() {
        while(char.IsDigit(cur))
            _pos++;

        var len = _pos - _start;
        var text = _text.Substring(_start, len);

        if(!int.TryParse(text, out var val))
            _diags.report_invalid_num(new textspan(_start, len), _text, typeof(int));

        _val = val;
        _type = syntype.numtok;
    }
}