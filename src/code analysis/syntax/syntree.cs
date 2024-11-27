using System.Collections.Immutable;

public sealed class syntree {
    public syntree(ImmutableArray<diag> diags, exprsyn root, syntok eoftok) {
        this.root = root;
        this.eoftok = eoftok;
        this.diags = diags;
    }

    public ImmutableArray<diag> diags { get; }
    public exprsyn root { get; }
    public syntok eoftok { get; }

    public static syntree parse(string text) {
        var parser = new parser(text);
        return parser.parse();
    }

    public static IEnumerable<syntok> parsetoks(string text) {
        var lexer = new lexer(text);

        while(true) {
            var tok = lexer.lex();

            if(tok.type == syntype.eoftok)
                break;

            yield return tok;
        }
    }
}
