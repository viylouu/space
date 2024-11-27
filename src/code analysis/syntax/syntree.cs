using System.Collections.Immutable;

public sealed class syntree {
    public syntree(srctext text, ImmutableArray<diag> diags, exprsyn root, syntok eoftok) {
        this.root = root;
        this.eoftok = eoftok;
        this.diags = diags;
        this.text = text;
    }

    public srctext text { get; }
    public ImmutableArray<diag> diags { get; }
    public exprsyn root { get; }
    public syntok eoftok { get; }

    public static syntree parse(string text) {
        var _srctext = srctext.from(text);
        return parse(_srctext);
    }

    public static syntree parse(srctext text) {
        var parser = new parser(text);
        return parser.parse();
    }

    public static IEnumerable<syntok> parsetoks(string text) {
        var _srctext = srctext.from(text);
        return parsetoks(_srctext);
    }

    public static IEnumerable<syntok> parsetoks(srctext text) {
        var lexer = new lexer(text);

        while(true) {
            var tok = lexer.lex();

            if(tok.type == syntype.eoftok)
                break;

            yield return tok;
        }
    }
}
